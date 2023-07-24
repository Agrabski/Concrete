using Concrete.Core.Courses;
using Concrete.Core.Services;
using Concrete.Core.Services.Courses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Concrete.Server.Controllers;
[Route("api/Course/Template")]
[ApiController]
public class CourseTemplateController : ControllerBase
{
	private readonly IConcreteUnitOfWork _unitOfWork;
	private readonly ICourseService _courseService;

	public CourseTemplateController(ICourseService courseService, IConcreteUnitOfWork unitOfWork)
	{
		_courseService = courseService;
		_unitOfWork = unitOfWork;
	}

	[HttpPost("/Create")]
	public async Task<CourseTemplate> CreateTemplateAsync(string name, CancellationToken token)
	{
		var result = await _courseService.CreateCourseTemplateAsync(name, token);
		await _unitOfWork.CommitAsync(token);
		return result;
	}
}
