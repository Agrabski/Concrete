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
			return Ok(data.Value.RootElement);
		return NotFound("The given key does not exist for this extension");
	}

	[HttpPost("{extensionName}/{category}/{key}")]
	public async Task InsertAsync(ExtensionName extensionName, string category, string key, [FromBody] JsonDocument data)
	{
		await context.ExtensionData.AddAsync(new()
		{
			Category = category,
			ExtensionName = extensionName,
			Key = key,
			Value = data,
			Modified = DateTime.UtcNow
		});
		await context.SaveChangesAsync();
	}

	[HttpGet("{extensionName}/list/{category}")]
	public IAsyncEnumerable<string> GetKeysInExtensionDataCategoryAsync(ExtensionName extensionName, string category, [FromQuery] int skip, [FromQuery] int take)
	{
		return context
			.ExtensionData
			.OrderBy(e => e.Modified)
			.Where(e => e.ExtensionName == extensionName && e.Category == category)
			.Select(e => e.Key)
			.Skip(skip)
			.Take(take)
			.AsAsyncEnumerable()
			;
	}
}
