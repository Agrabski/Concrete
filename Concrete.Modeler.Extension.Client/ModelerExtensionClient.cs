using Concrete.Core.Template;
using Concrete.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json;

namespace Concrete.Modeler.Extension.Client;

internal class ModelerExtensionClient(
	HttpClient client,
	IOptions<ModelerExtensionOptions> options,
	ILogger<ModelerExtensionClient> logger) : IModelerExtensionClient
{
	private static readonly ActivitySource _activitySource = new(nameof(ModelerExtensionClient));
	private Dictionary<ExtensionName, Uri>? _extensionUris;
	public async Task<ActivityMetadata[]> GetAllActivitiesAsync(CancellationToken token)
	{
		using var _ = _activitySource.StartActivity(nameof(GetAllActivitiesAsync));
		var config = options.Value;
		logger.LogDebug("Calling {Extension Endpoint Count} extension endpoints for activity metadata", config.ExtensionAddresses.Count);
		return (await Task.WhenAll(
			config
				.ExtensionAddresses
				.Select(uri => GetActivityMetadataFromExtensionAsync(uri, token)))
		)
		.SelectMany(x => x)
		.ToArray();
	}

	public async Task<ActivityTemplate> CreateTemplateAsync(ActivityName name, CancellationToken token)
	{
		using var _ = _activitySource.StartActivity();
		var uri = await GetExtensionUriAsync(name.Extension, token);
		return await CreateTemplateAsync(uri, name, token);
	}

	private async Task<ActivityTemplate> CreateTemplateAsync(Uri uri, ActivityName name, CancellationToken token)
	{
		using var _ = _activitySource.StartActivity();
		var response = await client.GetAsync(uri + $"api/activities/instance/{name}", token);
		if (response.IsSuccessStatusCode)
		{
			var stream= await response.Content.ReadAsStreamAsync(token);
			return await JsonSerializer.DeserializeAsync<ActivityTemplate>(stream, cancellationToken:token)
				??throw new Exception();
		}
		throw new NonSuccessApiResponseException(response.StatusCode);
	}

	private async ValueTask<Uri> GetExtensionUriAsync(ExtensionName name, CancellationToken token)
	{
		using var _ = _activitySource.StartActivity();
		if (_extensionUris is null)
		{
			var result = await Task.WhenAll(
				options
				.Value
				.ExtensionAddresses
				.Select(async uri => (await GetExtensionNameAsync(uri, token), uri))
			);
			_extensionUris = result.ToDictionary(kv => kv.Item1, kv => kv.uri);

		}
		return _extensionUris[name];
	}

	private async Task<ExtensionName> GetExtensionNameAsync(Uri uri, CancellationToken token)
	{
		using var _ = _activitySource.StartActivity(nameof(GetExtensionNameAsync));
		var response = await client.GetAsync(uri + "api/activities/name", token);
		if (response.IsSuccessStatusCode)
		{
			var text = await response.Content.ReadAsStringAsync(token);
			return ExtensionName.Parse(text, null);
		}
		throw new NonSuccessApiResponseException(response.StatusCode);

	}

	private async Task<ActivityMetadata[]> GetActivityMetadataFromExtensionAsync(Uri extensionUri, CancellationToken token)
	{
		using var activity = _activitySource.StartActivity(nameof(GetActivityMetadataFromExtensionAsync));
		activity?.SetTag("url", extensionUri);
		var response = await client.GetAsync(extensionUri + "api/activities", token);
		if (response.IsSuccessStatusCode)
		{
			var stream = response.Content.ReadAsStream(token);
			return await JsonSerializer.DeserializeAsync<ActivityMetadata[]>(
				stream,
				cancellationToken: token
			) ?? throw new InvalidOperationException("Content did not contain activity metadata");
		}
		throw new NonSuccessApiResponseException(response.StatusCode);
	}
}

