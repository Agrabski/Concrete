using Concrete.Core.Template;
using System.Text.Json;

namespace Concrete.Core.Data.Api.Client;

public interface IDataClient
{
	public Task UpdateActivityTemplateContent(Guid activityId, JsonElement data, CancellationToken token);
	public Task<ActivityTemplate> GetActivityTemplate(Guid actityId, CancellationToken token);
}
