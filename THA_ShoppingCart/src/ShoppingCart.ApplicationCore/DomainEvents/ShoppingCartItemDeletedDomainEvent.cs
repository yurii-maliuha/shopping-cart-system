using ShoppingCart.ApplicationCore.Primitives;

namespace ShoppingCart.ApplicationCore.DomainEvents;

public sealed record ShoppingCartItemDeletedDomainEvent(Guid CartItemId, Guid CartId) : IDomainEvent
{
}
