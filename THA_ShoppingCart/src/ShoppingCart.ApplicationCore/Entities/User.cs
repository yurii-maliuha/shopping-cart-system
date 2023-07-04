using ShoppingCart.ApplicationCore.Primitives;

namespace ShoppingCart.ApplicationCore.Entities;

public class User : Entity
{
    public string FullName { get; private set; }
    public string Email { get; set; }

    public User(Guid id, string fullName, string email)
        : base(id)
    {
        FullName = fullName;
        Email = email;
    }
}
