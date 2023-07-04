using ShoppingCart.ApplicationCore.Primitives;

namespace ShoppingCart.ApplicationCore.DomainEvents;

public sealed record CartClearedDomainEvent(Guid CartId) : IDomainEvent
{
}
