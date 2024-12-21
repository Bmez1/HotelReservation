using Common;

namespace HotelReservation.Domain.Entities;

public sealed class User : EntityBase<Guid>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }

    public List<Role> Roles { get; set; } = [];

    private User(Guid id, string userName, string email, string firstName, string lastName, string passwordHash, DateTime createdAt)
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
        Email = email;
    }

    public static User Create(string userName, string email, string firstName, string lastName, string passwordHash) => new        (Guid.NewGuid(), userName, email, firstName, lastName, passwordHash, DateTime.UtcNow);
}
