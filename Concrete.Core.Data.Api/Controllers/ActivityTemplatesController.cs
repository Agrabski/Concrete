using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Concrete.Core.Data.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ActivityTemplatesController(ConcreteContext context) : ControllerBase
{
	[HttpPost("{activityId:guid}")]
	public async Task<ActionResult> UpdateActivityTemplateContentAsync(
		Guid activityId,
		[FromBody] JsonDocument content,
		CancellationToken token
	)
	{
		if (await context
			.ActivityTemplates
			.Where(a => a.Id == activityId)
			.ExecuteUpdateAsync(a => a.SetProperty(t => t.Data, _ => content), token) == 1
		)
			return Ok();
		else
			return NotFound();
	}
}
