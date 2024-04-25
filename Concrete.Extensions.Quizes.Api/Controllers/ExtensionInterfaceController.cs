using Concrete.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Extensions.Quizes.Api.Controllers;
[Route("api/activities")]
[ApiController]
public class ExtensionInterfaceController : ControllerBase
{
	[HttpGet]
	public Task<ActivityMetadata[]> GetActivityMetadata(CancellationToken cancellationToken)
	{
		return Task.FromResult(new ActivityMetadata[]
		{
			new(new(MetadataConsts.ExtensionName, "Quiz"))
		});
	}

	[HttpGet("name")]
	public ExtensionName GetName() => MetadataConsts.ExtensionName;

	[HttpGet("instance/{name}")]
	public ActionResult<QuizTemplate> CreateQuizTemplate(ActivityTypeName name)
	{
		if (name != QuizTemplate.ActivityName)
			return NotFound($"Activity {name} is not supported by this extension");
		return Ok(new QuizTemplate());
	}
}
