using ShoppingCart.ApplicationCore.Primitives;

namespace ShoppingCart.ApplicationCore.DomainEvents;

public sealed record ShoppingCartClearedDomainEvent(Guid CartId) : IDomainEvent
{
}
