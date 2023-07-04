﻿using ShoppingCart.ApplicationCore.DomainEvents;
using ShoppingCart.ApplicationCore.Exceptions;
using ShoppingCart.ApplicationCore.Primitives;
using ShoppingCart.ApplicationCore.ValueObjects;

namespace ShoppingCart.ApplicationCore.Entities;

public class Cart : AgregateRoot
{
    private const string DEFAULT_CURRENCY = "USD";
    public Guid BuyerId { get; private set; }

    private readonly IList<CartItem> _items;
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public Money Total
    {
        get
        {
            var currencyCode = _items.FirstOrDefault()?.UnitPrice.Currency ?? DEFAULT_CURRENCY;
            var amount = _items?.Sum(i => i.Quantity * i.UnitPrice.Amount) ?? 0;
            return new Money(amount, currencyCode);
        }
    }

    public Cart(Guid id, Guid buyerId, IList<CartItem> items)
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

        CartItem addedItem;
        if (!_items.Any(i => i.ProductId == product.Id))
        {
            addedItem = CartItem.Create(Guid.NewGuid(), product, quantity: 1);
            _items.Add(addedItem);
            return;
        }

        addedItem = UpdateItem(product, quantity);

        RaiseDomainEvent(new CartItemAddedDomainEvent(CartItemId: addedItem.Id, CartId: Id));
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

    private CartItem UpdateItem(Product product, int quantity)
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

            RaiseDomainEvent(new CartItemDeletedDomainEvent(CartItemId: itemToDelete.Id, CartId: Id));
        }
    }

    public void Clear()
    {
        _items.Clear();

        RaiseDomainEvent(new CartClearedDomainEvent(CartId: Id));
    }


}
