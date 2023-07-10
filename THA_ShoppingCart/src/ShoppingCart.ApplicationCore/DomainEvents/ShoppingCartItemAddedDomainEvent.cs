using ShoppingCart.ApplicationCore.Primitives;

namespace ShoppingCart.ApplicationCore.DomainEvents;

public sealed record ShoppingCartItemAddedDomainEvent(Guid CartItemId, Guid CartId) : IDomainEvent
{
}
