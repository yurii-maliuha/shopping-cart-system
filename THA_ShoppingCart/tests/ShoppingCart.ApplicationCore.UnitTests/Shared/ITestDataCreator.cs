namespace ShoppingCart.ApplicationCore.UnitTests.Shared;

public interface ITestDataCreator<T>
{
    T Create(int index);
}
