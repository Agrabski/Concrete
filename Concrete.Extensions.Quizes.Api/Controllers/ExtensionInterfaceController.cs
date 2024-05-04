using Concrete.Core.Template;
using Concrete.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Concrete.Extensions.Quizes.Api.Controllers;
[Route("api")]
[ApiController]
public class ExtensionInterfaceController(IOptions<QuizesConfiguration> options) : ControllerBase
{
	[HttpGet("activities")]
	public ActivityMetadata[] GetActivityMetadata()
	{
		return
		[
			new(new(MetadataConsts.ExtensionName(), "Quiz"))
		];
	}

	[HttpGet("activities/name")]
	public ExtensionName GetName() => MetadataConsts.ExtensionName();

	[HttpGet("activities/instance/{name}")]
	public ActionResult<ActivityTemplate> CreateQuizTemplate(ActivityTypeName name)
	{
		if (name != MetadataConsts.QuizActivityName())
			return NotFound($"Activity {name} is not supported by this extension");
		return Ok(new ActivityTemplate()
		{
			Discriminator = MetadataConsts.QuizActivityName(),
			Name = "New quiz",
			Id = Guid.NewGuid(),
			DisplayName = new(),
			Data = JsonDocument.Parse("{}")
		});
	}

	[HttpGet("activities/editor/{name}")]
	public ActionResult<Uri> GetExtensionEditor(ActivityTypeName name)
	{
		if (name != MetadataConsts.QuizActivityName())
			return NotFound("Unknown activity type");
		return Ok(options.Value.ActivityEditorUri);
	}

	[HttpGet("menus")]
	public MenuMetadata[] GetMenuMetadata()
	{
		return [
			new("Questions", options.Value.QuestionsMenuUri)
		];
	}
}
