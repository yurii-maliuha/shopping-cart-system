using ShoppingCart.ApplicationCore.DomainEvents;
using ShoppingCart.ApplicationCore.Enums;
using ShoppingCart.ApplicationCore.Exceptions;
using ShoppingCart.ApplicationCore.Primitives;
using ShoppingCart.ApplicationCore.ValueObjects;

namespace ShoppingCart.ApplicationCore.Entities;

public class ShoppingCart : AgregateRoot
{
    private const CurrencyCode DEFAULT_CURRENCY = CurrencyCode.USD;
    public Guid BuyerId { get; private set; }

    private readonly IList<ShoppingCartItem> _items;
    public IReadOnlyCollection<ShoppingCartItem> Items => _items.AsReadOnly();

    public Money Total
    {
        get
        {
            var currencyCode = _items.FirstOrDefault()?.UnitPrice.Currency ?? DEFAULT_CURRENCY;
            var totalPrice = new Money(0, currencyCode);
            totalPrice = _items.Aggregate(totalPrice, (acc, i) => acc + i.UnitPrice * i.Quantity);
            return totalPrice;
        }
    }

    public ShoppingCart(Guid id, Guid buyerId, IList<ShoppingCartItem> items)
        : base(id)
    {
        BuyerId = buyerId;
        _items = items;
    }

    public void AddItem(Product product, int quantity = 1)
    {
        if (quantity < 1)
        {
            throw new InvalidQuantityValueOfCartItemsDomainException($"The {nameof(quantity)} parameter should be greater or equal to 1");
        }

        ShoppingCartItem addedItem;
        if (!_items.Any(i => i.ProductId == product.Id))
        {
            addedItem = ShoppingCartItem.Create(Guid.NewGuid(), product, quantity: 1);
            _items.Add(addedItem);
            return;
        }

        addedItem = UpdateItem(product, quantity);

        RaiseDomainEvent(new ShoppingCartItemAddedDomainEvent(CartItemId: addedItem.Id, CartId: Id));
    }

    public void RemoveItem(Product product, int quantity = 1)
    {
        if (quantity < 1)
        {
            throw new InvalidQuantityValueOfCartItemsDomainException($"The {nameof(quantity)} parameter should be greater or equal to 1");
        }

        var itemToRemove = UpdateItem(product, -quantity);
        if (itemToRemove.Quantity <= 0)
        {
            DeleteItem(product);
        }
    }

    private ShoppingCartItem UpdateItem(Product product, int quantity)
    {
        var itemToUpdate = Items.FirstOrDefault(i => i.ProductId == product.Id);
        if (itemToUpdate is null)
        {
            throw new NotFoundCartItemOnUpdateDomainException("Item can not be undefined for this operation");
        }

        itemToUpdate.AddQuantity(quantity);
        return itemToUpdate;
    }

    public void DeleteItem(Product product)
    {
        var itemToDelete = Items.FirstOrDefault(i => i.ProductId == product.Id);
        if (itemToDelete is not null)
        {
            _items.Remove(itemToDelete);

            RaiseDomainEvent(new ShoppingCartItemDeletedDomainEvent(CartItemId: itemToDelete.Id, CartId: Id));
        }
    }

    public void Clear()
    {
        _items.Clear();

        RaiseDomainEvent(new ShoppingCartClearedDomainEvent(CartId: Id));
    }
}
