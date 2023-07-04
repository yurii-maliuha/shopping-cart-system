using ShoppingCart.ApplicationCore.Primitives;
using ShoppingCart.ApplicationCore.ValueObjects;

namespace ShoppingCart.ApplicationCore.Entities;

public class CartItem : Entity
{
    public Guid ProductId { get; private set; }
    public Money UnitPrice { get; set; }
    public int Quantity { get; private set; }

    public CartItem(Guid id, Guid productId, Money unitPrice, int quantity)
        : base(id)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public static CartItem Create(Guid id, Product product, int quantity)
    {
        return new CartItem(id, product.Id, product.Price, quantity);
    }

    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }
}
