using Auth.Domain.Entities;
using Auth.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence
{
    public class AuthContextImp : DbContext, IAuthContext
    {
        public AuthContextImp(DbContextOptions<AuthContextImp> options) : base(options) { }
        public DbSet<Login> Logins { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<JWTTokenResponse> JWTTokenResponses { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
