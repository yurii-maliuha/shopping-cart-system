using ShoppingCart.ApplicationCore.Enums;
using ShoppingCart.ApplicationCore.Primitives;
using ShoppingCart.ApplicationCore.ValueObjects;

namespace ShoppingCart.ApplicationCore.Entities;

public class Product : Entity
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Band { get; init; }
    public Money Price { get; init; }
    public Size Size { get; init; }

    public Product(Guid id, string name, string description, string band, Money price, Size size)
        : base(id)
    {
        Name = name;
        Description = description;
        Band = band;
        Price = price;
        Size = size;
    }
}
