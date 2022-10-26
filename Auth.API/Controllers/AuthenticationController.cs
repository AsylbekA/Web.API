using Auth.Application.Helper;
using Auth.Application.Models;
using Auth.Application.Models.Interfaces;
using Auth.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
    private IUserService _userService;

    public AuthenticationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequestImp model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
}
