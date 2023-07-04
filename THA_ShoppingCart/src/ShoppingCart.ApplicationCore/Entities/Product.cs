using ShoppingCart.ApplicationCore.Enums;
using ShoppingCart.ApplicationCore.Primitives;
using ShoppingCart.ApplicationCore.ValueObjects;

namespace ShoppingCart.ApplicationCore.Entities;

public class Product : Entity
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Band { get; init; }
    public string Category { get; init; }
    public Money Price { get; init; }
    public Size Size { get; init; }

    public Product(Guid id, string name, string description, string band, string category, Money price, Size size)
        : base(id)
    {
        Name = name;
        Description = description;
        Band = band;
        Category = category;
        Price = price;
        Size = size;
    }
}
