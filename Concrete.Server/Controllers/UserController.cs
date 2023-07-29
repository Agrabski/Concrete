using Concrete.Server.Requests;
using Concrete.Storage.EfCore;
using Concrete.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly SignInManager<IAuthenticatedUser> _signInManager;
	private readonly UserManager<IAuthenticatedUser> _userManager;
	private readonly PasswordHasher<IAuthenticatedUser> _passwordHasher;

	public UserController(
		SignInManager<IAuthenticatedUser> signInManager,
		UserManager<IAuthenticatedUser> userManager,
		PasswordHasher<IAuthenticatedUser> passwordHasher)
	{
		_signInManager = signInManager;
		_userManager = userManager;
		_passwordHasher = passwordHasher;
	}

	[HttpPost]
	public async Task<ActionResult> AuthorizeAsync([FromBody] AuthorizeRequestBody request)
	{
		var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);
		if (!result.Succeeded)
			return Unauthorized("Invalid username or password");
		return Ok();
	}

	[HttpPost("create"), Authorize(Roles = nameof(UserRole.Admin))]
	public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserRequest request)
	{
		var user = new DatabaseUser()
		{
			Name = request.Username,
			Id = Guid.NewGuid(),
			Role = request.Role,
			Surname = request.Surname,
			UserName = request.Username,
			AuthenticationInfo = new()
			{
				Email = request.Email,
				PasswordHash = string.Empty
			}
		};
		user.AuthenticationInfo.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
		var result = await _userManager.CreateAsync(user);
		if (result.Succeeded)
			return Ok(user.Id);
		return Unauthorized(result.Errors);
	}
}
