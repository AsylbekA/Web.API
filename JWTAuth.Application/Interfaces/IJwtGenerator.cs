using JWTAuth.Domain.Persistence;

namespace JWTAuth.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
