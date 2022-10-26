using Auth.Application.Cache.Redis.Interfaces;
using Auth.Application.Cache.Redis.RedisHelper;
using Auth.Application.Helper;
using Auth.Application.Models;
using Auth.Application.Services.Interfaces;
using Auth.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Auth.Application.Services;

public class UserService : IUserService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<Login> _userLogin = new()
    {
        new Login { Id = 1,Phone="+77000229300",  PasswordHash = "test"}
    };

    private readonly AppSettings _appSettings;
    private readonly ICacheService _cache;
    public UserService(IOptions<AppSettings> appSettings, ICacheService cache)
    {
        _appSettings = appSettings.Value;
        _cache = cache;
    }

    public AuthenticateResponseImp Authenticate(AuthenticateRequestImp model)
    {
        var user = _userLogin.SingleOrDefault(x => x.Phone == model.Username && x.PasswordHash == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);
        DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(60.0);
        _cache.SetData(AuthGenerateCacheKeys.Authorization + user.Phone, token, expirationTime);
        _cache.SetData(user.Phone, user, expirationTime);
        return new AuthenticateResponseImp(user, token);
    }

    public IEnumerable<Login> GetAll()
    {
        return _userLogin;
    }

    public Login? GetById(int Id)
    {
        return _userLogin.FirstOrDefault(x => x.Id == Id);
    }

    // helper methods

    private string generateJwtToken(Login user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
