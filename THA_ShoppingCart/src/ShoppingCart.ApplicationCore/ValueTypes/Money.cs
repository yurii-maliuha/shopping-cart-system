namespace ShoppingCart.ApplicationCore.ValueTypes;

public record Money
{
    public int Amount { get; private set; }
    public string Currency { get; private set; }

    public Money(int amount, string currencyCode)
    {
        Amount = amount;
        Currency = currencyCode;
    }
}
