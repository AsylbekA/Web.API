using Auth.Domain.Entities.BaseEntities;

namespace Auth.Domain.Entities;

public class Login : BaseEntity
{
    public User Users { get; set; }
    public string Phone { get; set; }

    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime LastLoginTime { get; set; } = DateTime.UtcNow.AddHours(6);
    public DateTime LastUpdatePassword { get; set; }
    public bool IsBlocked { get; set; } = false;


}
