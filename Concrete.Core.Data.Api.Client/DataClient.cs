using Concrete.Core.Template;
using System.Net.Http.Json;
using System.Text.Json;

namespace Concrete.Core.Data.Api.Client;

internal sealed class DataClient(HttpClient client) : IDataClient
{
	public async Task<ActivityTemplate> GetActivityTemplate(Guid actityId, CancellationToken token)
	{
		var result = await client.GetFromJsonAsync<ActivityTemplate>(new Uri($"api/activitytemplate/{actityId}", UriKind.Relative), token);
		return result ?? throw new Exception("No data in response");
	}

	public Task UpdateActivityTemplateContent(Guid activityId, JsonElement data, CancellationToken token) => throw new NotImplementedException();
}
