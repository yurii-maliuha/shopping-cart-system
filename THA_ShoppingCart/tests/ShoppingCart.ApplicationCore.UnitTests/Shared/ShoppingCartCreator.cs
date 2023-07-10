namespace ShoppingCart.ApplicationCore.UnitTests.Shared;

public class ShoppingCartCreator : ITestDataCreator<Entities.ShoppingCart>
{
    public Entities.ShoppingCart Create(int index)
    {
        return new Entities.ShoppingCart(
            id: Guid.NewGuid(),
            buyerId: Guid.NewGuid(),
            items: Fixture.CreateMany(3, new ShoppingCartItemCreator()));
    }
}
