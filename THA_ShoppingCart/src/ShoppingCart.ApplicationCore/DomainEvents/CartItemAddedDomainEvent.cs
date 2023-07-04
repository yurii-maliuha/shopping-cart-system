using ShoppingCart.ApplicationCore.Primitives;

namespace ShoppingCart.ApplicationCore.DomainEvents;

public sealed record CartItemAddedDomainEvent(Guid CartItemId, Guid CartId) : IDomainEvent
{
}
