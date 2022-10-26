using Auth.Domain.Entities.BaseEntities;

namespace Auth.Domain.Entities;

public class JWTTokenResponse:BaseEntity
{
    public string Token { get; set; }
}
