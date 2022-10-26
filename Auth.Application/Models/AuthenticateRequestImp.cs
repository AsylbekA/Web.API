using Auth.Application.Models.Interfaces;

namespace Auth.Application.Models
{
    public class AuthenticateRequestImp: IAuthenticateRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
