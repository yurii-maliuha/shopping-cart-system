namespace ShoppingCart.ApplicationCore.Exceptions;

public sealed class InvalidQuantityValueOfCartItemsDomainException : DomainException
{
    public InvalidQuantityValueOfCartItemsDomainException(string message) : base(message)
    {
    }
}
