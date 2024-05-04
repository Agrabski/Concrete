using Concrete.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Concrete.Core.Data.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExtensionDataController(ConcreteContext context) : ControllerBase
{
	[HttpGet("{extensionName}/{key}")]
	public async Task<ActionResult<JsonDocument>> GetData(ExtensionName extensionName, string key)
	{
		if (await context
			.ExtensionData
			.FirstOrDefaultAsync(e =>
				e.ExtensionName == extensionName &&
				e.Key == key
			) is ExtensionData data
		)
			return Ok(data);
		return NotFound("The given key does not exist for this extension");
	}

	[HttpGet("{extensionName}/list/{category}")]
	public IAsyncEnumerable<string> GetKeysInExtensionDataCategoryAsync(ExtensionName extensionName, string category)
	{
		return context
			.ExtensionData
			.Where(e => e.ExtensionName == extensionName && e.Category == category)
			.Select(e => e.Key)
			.AsAsyncEnumerable()
			;
	}
}
