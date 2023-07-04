namespace ShoppingCart.ApplicationCore.Exceptions;

public sealed class CurrencyMismatchMoneyDomainException : DomainException
{
    public CurrencyMismatchMoneyDomainException(string message) : base(message)
    {
    }
}
