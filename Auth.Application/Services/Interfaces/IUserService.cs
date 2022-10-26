using Auth.Application.Models;
using Auth.Domain.Entities;

namespace Auth.Application.Services.Interfaces
{
    public interface IUserService
    {
        //IAuthenticateResponse Authenticate(IAuthenticateRequest model);
        AuthenticateResponseImp Authenticate(AuthenticateRequestImp model);
        IEnumerable<Login> GetAll();
        Login GetById(int Id);
    }
}
