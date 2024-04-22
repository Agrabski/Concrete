using Concrete.Core.Template;
using Concrete.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json;

namespace Concrete.Modeler.Client;

internal class ModelerClient(
	HttpClient client,
	IOptions<ModelerClientOptions> options,
	ILogger<ModelerClient> logger) : IModelerClient
{
	private readonly JsonSerializerOptions _options = new(JsonSerializerOptions.Default)
	{
		PropertyNameCaseInsensitive = false,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	};
	private static readonly ActivitySource _activitySource = new("Concrete.Modeler.Extension.Client");
	public async Task<ActivityMetadata[]> GetAllActivitiesAsync(CancellationToken token)
	{
		using var activity = _activitySource.StartActivity();
		var modelerUri = options.Value.ModelerUri;
		logger.LogDebug("Calling {Modeler endpointy} endpoint for activity metadata", modelerUri);
		activity?.SetTag("url", modelerUri);

		var response = await client.GetAsync(modelerUri + "api/activities", token);
		if (response.IsSuccessStatusCode)
		{
			var stream = response.Content.ReadAsStream(token);
			if (logger.IsEnabled(LogLevel.Debug))
				logger.LogDebug("Recieved message: {Message}", await response.Content.ReadAsStringAsync(token));
			return await JsonSerializer.DeserializeAsync<ActivityMetadata[]>(
				stream,
				_options,
				cancellationToken: token
			) ?? throw new InvalidOperationException("Content did not contain activity metadata");
		}
		throw new NonSuccessApiResponseException(response.StatusCode);
	}

	public async Task<CourseTemplate> CreateCourseTemplateAsync(CancellationToken token)
	{
		using var _ = _activitySource.StartActivity();
		var response = await client.PostAsync(options.Value.ModelerUri + $"api/CourseTemplates/", null, token);
		if (response.IsSuccessStatusCode)
		{
			var stream = await response.Content.ReadAsStreamAsync(token);
			return await JsonSerializer.DeserializeAsync<CourseTemplate>(stream, _options, cancellationToken: token)
				?? throw new Exception();
		}
		throw new NonSuccessApiResponseException(response.StatusCode);
	}
}

