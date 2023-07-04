using ShoppingCart.ApplicationCore.Entities;
using ShoppingCart.ApplicationCore.Enums;

namespace ShoppingCart.ApplicationCore.UnitTests.Shared;

public class ProductCreator : ITestDataCreator<Product>
{
    private MoneyCreator _moneyCreator;
    public ProductCreator()
    {
        _moneyCreator = new MoneyCreator();
    }

    public Product Create(int index)
    {
        return new Product(
            id: Guid.NewGuid(),
            name: $"T-Shirt Name {index}",
            description: $"T-Shirt Description {index}",
            band: $"Band {index}",
            price: _moneyCreator.Create(index),
            size: Size.Medium);
    }
}
