namespace ShoppingCart.ApplicationCore.Primitives;

public abstract class AgregateRoot : Entity
{
    private readonly IList<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    protected AgregateRoot()
        : base()
    {

    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
