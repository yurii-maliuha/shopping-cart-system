using ShoppingCart.ApplicationCore.Entities;

namespace ShoppingCart.ApplicationCore.UnitTests.Shared;

public class ShoppingCartItemCreator : ITestDataCreator<ShoppingCartItem>
{
    private MoneyCreator _moneyCreator;
    public ShoppingCartItemCreator()
    {
        _moneyCreator = new MoneyCreator();
    }

    public ShoppingCartItem Create(int index)
    {
        Random random = new Random();
        return new ShoppingCartItem(
            id: Guid.NewGuid(),
            productId: Guid.NewGuid(),
            unitPrice: _moneyCreator.Create(index),
            quantity: random.Next(1, 5));
    }
}
