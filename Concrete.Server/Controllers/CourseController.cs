using Concrete.Core.Services.Courses;
using Concrete.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
	private readonly UserManager<IAuthenticatedUser> _userManager;
	private readonly ICourseRepository _courseRepository;

	public CourseController(ICourseRepository courseRepository, UserManager<IAuthenticatedUser> userManager)
	{
		_courseRepository = courseRepository;
		_userManager = userManager;
	}

	[HttpGet, Authorize()]
	public async Task<ActionResult<CourseHeader[]>> GetCoursesForStudentAsync(CancellationToken token)
	{
		var userId = await _userManager.GetUserAsync(HttpContext.User);
		if (userId is null)
			return Unauthorized();
		return Ok(await _courseRepository.ListCoursesForStudentAsync(userId.Id, token).ToArrayAsync(token));
	}
}
