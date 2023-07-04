using ShoppingCart.ApplicationCore.Enums;

namespace ShoppingCart.ApplicationCore.ValueObjects;

public record Money
{
    public int Amount { get; private set; }
    public CurrencyCode Currency { get; private set; }

    public Money(int amount, CurrencyCode currencyCode)
    {
        Amount = amount;
        Currency = currencyCode;
    }
}
