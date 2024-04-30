namespace Concrete.Core.Data.Api.Client;

public interface IDataClient
{
	public Task UpdateActivityTemplateContent(Guid activityId, object data, CancellationToken token);
}
