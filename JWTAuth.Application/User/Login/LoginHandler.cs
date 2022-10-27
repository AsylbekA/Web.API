using JWTAuth.Application.Exceptions;
using JWTAuth.Application.Interfaces;
using JWTAuth.Domain.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Application.User.Login;

public class LoginHandler : IRequestHandler<LoginQuery, User>
{
	private readonly UserManager<AppUser> _userManager;

	private readonly SignInManager<AppUser> _signInManager;

	private readonly IJwtGenerator _jwtGenerator;

	public LoginHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_jwtGenerator = jwtGenerator;
	}

	public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);
		if (user == null)
		{
			throw new RestException(HttpStatusCode.Unauthorized);
		}

		var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

		if (result.Succeeded)
		{
			return new User
			{
				DisplayName = user.DisplayName,
				Token = _jwtGenerator.CreateToken(user),
				UserName = user.UserName,
				Image = null
			};
		}

		throw new RestException(HttpStatusCode.Unauthorized);
	}
}
