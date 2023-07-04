namespace ShoppingCart.ApplicationCore.Exceptions;

public sealed class NotFoundCartItemOnUpdateDomainException : DomainException
{
    public NotFoundCartItemOnUpdateDomainException(string message) : base(message)
    {
    }
}
