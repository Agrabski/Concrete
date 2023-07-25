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
	private readonly ICourseRepository _courseRepository;
	private readonly IConcreteUnitOfWork _unitOfWork;
	private readonly ICourseService _courseService;

	public CourseTemplateController(ICourseService courseService, IConcreteUnitOfWork unitOfWork, ICourseRepository courseRepository)
	{
		_courseService = courseService;
		_unitOfWork = unitOfWork;
		_courseRepository = courseRepository;
	}

	[HttpPost("/Create")]
	public async Task<CourseTemplate> CreateTemplateAsync(string name, CancellationToken token)
	{
		var result = await _courseService.CreateCourseTemplateAsync(name, token);
		await _unitOfWork.CommitAsync(token);
		return result;
	}

	[HttpPost]
	public async Task<ActionResult> UpdateTemplate(CourseTemplate template, CancellationToken token)
	{
		await _courseRepository.UpdateAsync(template, token);
		await _unitOfWork.CommitAsync(token);
		return Ok();
	}

	[HttpDelete]
	public async Task<ActionResult> DeleteTemplateAsync(Guid templateId, CancellationToken token)
	{
		await _courseRepository.DeleteAsync(templateId, token);
		return Ok();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<CourseTemplate>> GetAsync(Guid id, CancellationToken token)
	{
		var result = await _courseRepository.TryGetCourseTemplateAsync(id, token);
		if (result is not null)
			return Ok(result);
		return NotFound($"Course {id} was not found");
	}

	[HttpGet]
	public IAsyncEnumerable<CourseTemplateHeader> ListTemplatesAsync(CancellationToken token)
	{
		return _courseRepository.ListAsync(token);
	}

	[HttpPost("{templateId}")]
	public async Task<ActionResult<Guid>> CreateCourseFromTemplateAsync(
		[FromRoute] Guid templateId,
		[FromRoute] string courseCode,
		[FromBody] SubjectDate[] subjectDates,
		CancellationToken token
	)
	{
		var result = await _courseService.StartCourseAsync(templateId, subjectDates, courseCode, token);
		return Ok(result);
	}

}
