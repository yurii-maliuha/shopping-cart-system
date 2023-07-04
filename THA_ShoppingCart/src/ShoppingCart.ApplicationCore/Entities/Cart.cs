﻿using ShoppingCart.ApplicationCore.Interfaces;
using ShoppingCart.ApplicationCore.ValueTypes;

namespace ShoppingCart.ApplicationCore.Entities;

public class Cart : IAgregateRoot
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

    public Cart(Guid buyerId, IList<CartItem> items)
    {
        BuyerId = buyerId;
        _items = items;
    }

    public void AddItem(Product product, int quantity = 1)
    {
        if (quantity < 1)
        {
            throw new ArgumentOutOfRangeException("The quantity parameter should be greater or equal to 1");
        }

        if (!_items.Any(i => i.ProductId == product.Id))
        {
            _items.Add(CartItem.Create(product, quantity: 1));
            return;
        }

        var existingItem = Items.First(i => i.ProductId == product.Id);
        existingItem.AddQuantity(quantity);

        //TODO publish CartItemUpserted
    }

    public void RemoveItem(Product product)
    {
        var itemToRemove = Items.FirstOrDefault(i => i.ProductId == product.Id);
        if (itemToRemove is null)
        {
            throw new ArgumentNullException("Item can not be undefined for this operation");
        }

        _items.Remove(itemToRemove);

        //TODO publish CartItemUpserted
    }

    public void Clear()
    {
        _items.Clear();
        //TODO publish CartItemCleared
    }


}