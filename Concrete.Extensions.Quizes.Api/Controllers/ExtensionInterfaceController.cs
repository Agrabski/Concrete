using Concrete.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Concrete.Extensions.Quizes.Api.Controllers;
[Route("api/activities")]
[ApiController]
public class ExtensionInterfaceController(IOptions<QuizesConfiguration> options) : ControllerBase
{
	[HttpGet]
	public ActivityMetadata[] GetActivityMetadata()
	{
		return
		[
			new(new(MetadataConsts.ExtensionName(), "Quiz"))
		];
	}

	[HttpGet("name")]
	public ExtensionName GetName() => MetadataConsts.ExtensionName();

	[HttpGet("instance/{name}")]
	public ActionResult<QuizTemplate> CreateQuizTemplate(ActivityTypeName name)
	{
		if (name != QuizTemplate.ActivityName)
			return NotFound($"Activity {name} is not supported by this extension");
		return Ok(new QuizTemplate());
	}

	[HttpGet("editor/{name}")]
	public ActionResult<Uri> GetExtensionEditor(ActivityTypeName name)
	{
		if (name != QuizTemplate.ActivityName)
			return NotFound("Unknown activity type");
		return Ok(options.Value.ActivityEditorUri);
	}
}
