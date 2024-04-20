using Concrete.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Concrete.Modeler.Extension.Client;

internal class ModelerExtensionClient(
	HttpClient client,
	IOptionsSnapshot<ModelerExtensionOptions> options,
	ILogger<ModelerExtensionClient> logger) : IModelerExtensionClient
{
	public async Task<ActivityMetadata[]> GetAllActivitiesAsync(CancellationToken token)
	{
		var config = options.Value;
		logger.LogDebug("Calling {Extension Endpoint Count} extension endpoints for activity metadata", config.ExtensionAddresses.Count);
		return (await Task.WhenAll(
			config
				.ExtensionAddresses
				.Select(async uri =>
				{
					var response = await new ModelerExtensionApiClient(uri.ToString(), client).ActivitiesAsync(token);
					return response.Select(r => new ActivityMetadata(ActivityName.Parse(r.Name, null)));
				})
		))
		.SelectMany(x => x)
		.ToArray();
	}
}

