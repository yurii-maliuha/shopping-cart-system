using ShoppingCart.ApplicationCore.Enums;
using ShoppingCart.ApplicationCore.Exceptions;

namespace ShoppingCart.ApplicationCore.ValueObjects;

public record Money
{
    public decimal Amount { get; private set; }
    public CurrencyCode Currency { get; private set; }

    public Money(decimal amount, CurrencyCode currencyCode)
    {
        Amount = amount;
        Currency = currencyCode;
    }

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new CurrencyMismatchMoneyDomainException("The currency of both moneys should match for this operation");
        }

        return new Money(amount: left.Amount + right.Amount, currencyCode: left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new CurrencyMismatchMoneyDomainException("The currency of both moneys should match for this operation");
        }

        return new Money(amount: left.Amount - right.Amount, currencyCode: left.Currency);
    }

    public static Money operator *(Money left, int right)
    {
        return new Money(amount: left.Amount * right, currencyCode: left.Currency);
    }
}
