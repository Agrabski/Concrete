using Concrete.Core.Template;
using Concrete.Interface;
using Concrete.Modeler.Extension.Client;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Modeler.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ActivitiesController(IModelerExtensionClient client) : ControllerBase
{
	[HttpGet]
	public Task<ActivityMetadata[]> GetAvailableActivities(CancellationToken token)
	{
		return client.GetAllActivitiesAsync(token);
	}

	[HttpGet("instance/{name}")]
	public Task<ActivityTemplate> CreateActivityTemplate(ActivityTypeName name, CancellationToken token)
	{
		return client.CreateTemplateAsync(name, token);
	}

	[HttpGet("editor/{name}")]
	public async ValueTask<ActionResult<Uri>> GetActivityEditorUrl(ActivityTypeName name, CancellationToken token)
	{
		return Ok(await client.GetExtensionActivityEditorAsync(name, token));
	}
}
