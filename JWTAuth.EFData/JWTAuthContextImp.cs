using JWTAuth.Domain.Persistence;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.EFData;

public class JWTAuthContextImp : IdentityDbContext<AppUser>
{
    public JWTAuthContextImp(DbContextOptions<JWTAuthContextImp> options) : base(options) { }
}
