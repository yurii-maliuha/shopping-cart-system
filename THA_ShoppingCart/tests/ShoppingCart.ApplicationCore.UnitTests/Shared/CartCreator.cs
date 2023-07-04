using ShoppingCart.ApplicationCore.Entities;

namespace ShoppingCart.ApplicationCore.UnitTests.Shared;

public class CartCreator : ITestDataCreator<Cart>
{
    public Cart Create(int index)
    {
        return new Cart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: Fixture.CreateMany(3, new CartItemCreator()));
    }
}
