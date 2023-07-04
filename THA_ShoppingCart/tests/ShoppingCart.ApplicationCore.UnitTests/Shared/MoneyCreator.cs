using ShoppingCart.ApplicationCore.Enums;
using ShoppingCart.ApplicationCore.ValueObjects;

namespace ShoppingCart.ApplicationCore.UnitTests.Shared;

public class MoneyCreator : ITestDataCreator<Money>
{
    private const CurrencyCode DEFAULT_CURRENCY = CurrencyCode.USD;
    public Money Create(int index)
    {
        Random random = new Random();
        decimal amount = random.Next(1, 1000) + index * (decimal)random.NextDouble();
        return new Money(amount, DEFAULT_CURRENCY);
    }
}
