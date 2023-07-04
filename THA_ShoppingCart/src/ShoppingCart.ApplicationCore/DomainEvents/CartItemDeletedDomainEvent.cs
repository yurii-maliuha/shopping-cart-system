using ShoppingCart.ApplicationCore.Primitives;

namespace ShoppingCart.ApplicationCore.DomainEvents;

public sealed record CartItemDeletedDomainEvent(Guid CartItemId, Guid CartId) : IDomainEvent
{
}
