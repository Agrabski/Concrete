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
	public Task<ActivityTemplate> CreateQuizTemplate(ActivityName name, CancellationToken token)
	{
		return client.CreateTemplateAsync(name, token);
	}
}
