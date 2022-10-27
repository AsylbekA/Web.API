using JWTAuth.Application.User;
using JWTAuth.Application.User.Login;
using JWTAuth.Application.User.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.API.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginAsync(LoginQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("registration")]
        public async Task<ActionResult<User>> RegistrationAsync(RegistrationCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
