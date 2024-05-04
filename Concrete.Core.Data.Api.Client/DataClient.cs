using Concrete.Core.Template;
using Concrete.Interface;
using System.Net.Http.Json;
using System.Text.Json;

namespace Concrete.Core.Data.Api.Client;

internal sealed class DataClient(HttpClient client) : IDataClient
{
	public async Task<ActivityTemplate> GetActivityTemplate(Guid actityId, CancellationToken token)
	{
		var result = await client.GetFromJsonAsync<ActivityTemplate>($"api/ActivityTemplates/{actityId}", token);
		return result ?? throw new Exception("No data in response");
	}

	public Task UpdateActivityTemplateContent(Guid activityId, JsonElement data, CancellationToken token) => throw new NotImplementedException();

	public async Task<JsonDocument> GetExtensionData(ExtensionName extensionName, string key, CancellationToken token)
	{
		var result = await client.GetFromJsonAsync<JsonDocument>($"api/ExtensionData/{extensionName}/{key}", token);
		return result ?? throw new Exception("No data in response");
	}

	public IAsyncEnumerable<string> GetKeysInExtensionDataCategoryAsync(ExtensionName extensionName, string category, CancellationToken token)
	{
		return client.GetFromJsonAsAsyncEnumerable<string>($"api/ExtensionData/{extensionName}/list/{category}", token)!;
	}
}
