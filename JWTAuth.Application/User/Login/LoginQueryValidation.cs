using FluentValidation;

namespace JWTAuth.Application.User.Login;

public class LoginQueryValidation : AbstractValidator<LoginQuery>
{
	public LoginQueryValidation()
	{
		RuleFor(x => x.Email).NotEmpty();

		RuleFor(x => x.Password).NotEmpty();
	}
}
