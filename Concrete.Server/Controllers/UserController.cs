using Concrete.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly SignInManager<IAuthenticatedUser> _signInManager;

	public UserController(SignInManager<IAuthenticatedUser> signInManager)
	{
		_signInManager = signInManager;
	}

	[HttpPost]
	public async Task<ActionResult> AuthorizeAsync([FromBody] AuthorizeRequestBody requset)
	{
		var result = await _signInManager.PasswordSignInAsync(requset.Username, requset.Password, true, false);
		if (!result.Succeeded)
			return Unauthorized("Invalid username or password");
		return Ok();
	}
}

public record AuthorizeRequestBody(string Username, string Password);
