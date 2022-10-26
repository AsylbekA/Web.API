namespace Auth.Application.Models.Interfaces
{
    public interface IAuthenticateRequest
    {
        string Username { get; set; }

        string Password { get; set; }
    }
}
