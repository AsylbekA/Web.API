using Microsoft.AspNetCore.Identity;

namespace JWTAuth.Domain.Persistence
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
