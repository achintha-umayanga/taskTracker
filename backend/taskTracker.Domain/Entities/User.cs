using taskTracker.Domain.Common;

namespace taskTracker.Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    private User() {}

    public static User Create(
        string email,
        string username,
        string passwordHash
    )
    {
        return new User
        {
            Email = email,
            UserName = username,
            PasswordHash = passwordHash
        };
    }
}
