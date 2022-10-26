using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Domain.Persistence;

public interface IAuthContext
{
    DbSet<Login> Logins { get; set; }
    DbSet<UserRole> Roles { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<JWTTokenResponse> JWTTokenResponses { get; set; }
    Task<int> SaveChangesAsync();
}
