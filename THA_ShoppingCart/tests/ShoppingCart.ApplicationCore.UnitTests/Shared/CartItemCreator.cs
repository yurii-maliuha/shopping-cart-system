using ShoppingCart.ApplicationCore.Entities;

namespace ShoppingCart.ApplicationCore.UnitTests.Shared;

public class CartItemCreator : ITestDataCreator<CartItem>
{
    private MoneyCreator _moneyCreator;
    public CartItemCreator()
    {
        _moneyCreator = new MoneyCreator();
    }

    public CartItem Create(int index)
    {
        Random random = new Random();
        return new CartItem(
            id: Guid.NewGuid(),
            productId: Guid.NewGuid(),
            unitPrice: _moneyCreator.Create(index),
            quantity: random.Next(1, 5));
    }
}
