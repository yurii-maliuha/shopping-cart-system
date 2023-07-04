namespace ShoppingCart.ApplicationCore.Primitives;
public abstract class Entity
{
    public Guid Id { get; private init; }

    protected Entity(Guid id)
    {
        Id = id;
    }
}
