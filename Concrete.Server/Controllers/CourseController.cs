using Concrete.Core.Courses;
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

	[HttpGet, Authorize(Roles = nameof(UserRole.Student))]
	public async Task<ActionResult<CourseHeader[]>> GetCoursesForStudentAsync(CancellationToken token)
	{
		var user = await _userManager.GetUserAsync(HttpContext.User);
		if (user is null)
			return Unauthorized();
		return Ok(await _courseRepository.ListCoursesForStudentAsync(user.Id, token).ToArrayAsync(token));
	}

	[HttpGet("{courseId}"), Authorize(Roles = nameof(UserRole.Student))]
	public async Task<ActionResult<Course>> GetCourseAsync(Guid courseId, CancellationToken token)
	{
		var user = await _userManager.GetUserAsync(HttpContext.User);
		if (user is null)
			return Unauthorized();
		var result = await _courseRepository.TryGetCourseForUserAsync(courseId, user.Id, token);
		if (result is null)
			return NotFound();
		return Ok(result);

	}

}
