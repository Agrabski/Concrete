using Concrete.Core.Template;
using Concrete.Interface;
using System.Text.Json;

namespace Concrete.Core.Data.Api.Client;

public interface IDataClient
{
	public Task UpdateActivityTemplateContent(Guid activityId, JsonDocument data, CancellationToken token);
	public Task<ActivityTemplate> GetActivityTemplate(Guid actityId, CancellationToken token);
	Task<JsonDocument?> GetExtensionData(ExtensionName extensionName, string key, CancellationToken token);
	IAsyncEnumerable<string> GetKeysInExtensionDataCategoryAsync(ExtensionName extensionName, string category, int skip, int takeCount, CancellationToken token);
	Task InsertExtensionDataAsync(ExtensionName extensionName, string category, string id, JsonDocument data, CancellationToken token);
}
