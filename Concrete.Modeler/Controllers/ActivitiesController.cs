using Concrete.Core.Data;
using Concrete.Core.Template;
using Concrete.Interface;
using Concrete.Modeler.Extension.Client;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Modeler.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ActivitiesController(IModelerExtensionClient client, ConcreteContext context) : ControllerBase
{
	[HttpGet]
	public Task<ActivityMetadata[]> GetAvailableActivities(CancellationToken token)
	{
		return client.GetAllActivitiesAsync(token);
	}

	[HttpPost("instance/{name}")]
	public async Task<ActionResult<ActivityTemplate>> CreateActivityTemplate(
		[FromRoute]ActivityTypeName name,
		[FromBody]Guid classId,
		CancellationToken token
	)
	{
		var template = await client.CreateTemplateAsync(name, token);
		var @class = await context.ClassTemplates.FindAsync([classId], token);
		if (@class is null)
			return NotFound($"Class {classId} does not exist");
		@class.ActivityTemplates.Add(template);
		await context.SaveChangesAsync(token);
		return Ok(template);
	}

	[HttpGet("editor/{name}")]
	public async ValueTask<ActionResult<Uri>> GetActivityEditorUrl(ActivityTypeName name, CancellationToken token)
	{
		return Ok(await client.GetExtensionActivityEditorAsync(name, token));
	}
}
