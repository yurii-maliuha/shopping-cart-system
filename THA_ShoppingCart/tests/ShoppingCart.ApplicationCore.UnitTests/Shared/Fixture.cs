namespace ShoppingCart.ApplicationCore.UnitTests.Shared;

public static class Fixture
{
    public static T Create<T>(ITestDataCreator<T> creator)
    {
        return creator.Create(1);
    }

    public static List<T> CreateMany<T>(int count, ITestDataCreator<T> creator)
    {

        var results = new List<T>();
        for (int i = 0; i < count; i++)
        {
            results.Add(creator.Create(i));
        }

        return results;
    }
}
