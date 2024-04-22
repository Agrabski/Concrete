using Concrete.Core.Template;
using Concrete.Interface;
using Concrete.Interface.Templates;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json;

namespace Concrete.Modeler.Client;

internal class ModelerClient(
	HttpClient client,
	IOptions<ModelerClientOptions> options) : IModelerClient
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

		var response = await client.GetAsync(modelerUri + "api/activities", token);
		if (response.IsSuccessStatusCode)
		{
			var stream = response.Content.ReadAsStream(token);
			return await JsonSerializer.DeserializeAsync<ActivityMetadata[]>(
				stream,
				_options,
				cancellationToken: token
			) ?? throw new InvalidOperationException("Content did not contain activity metadata");
		}
		throw new NonSuccessApiResponseException(response.StatusCode);
	}

	public async Task<CourseTemplateHeader[]> GetCoureTemplatesAsync(CancellationToken token)
	{
		using var activity = _activitySource.StartActivity();
		var modelerUri = options.Value.ModelerUri;
		return await GetAsync<CourseTemplateHeader[]>(client, modelerUri + "api/CourseTemplates", token);

	}

	private async Task<T> GetAsync<T>(HttpClient client, string endpoint, CancellationToken token)
	{
		var response = await client.GetAsync(endpoint, token);
		if (response.IsSuccessStatusCode)
		{
			var stream = response.Content.ReadAsStream(token);
			return await JsonSerializer.DeserializeAsync<T>(
				stream,
				_options,
				cancellationToken: token
			) ?? throw new InvalidOperationException("Content did not course templates");
		}
		throw new NonSuccessApiResponseException(response.StatusCode);
	}

	public async Task<CourseTemplateHeader> CreateCourseTemplateAsync(CancellationToken token)
	{
		using var _ = _activitySource.StartActivity();
		var response = await client.PostAsync(options.Value.ModelerUri + $"api/CourseTemplates/", null, token);
		if (response.IsSuccessStatusCode)
		{
			var stream = await response.Content.ReadAsStreamAsync(token);
			return await JsonSerializer.DeserializeAsync<CourseTemplateHeader>(stream, _options, token);
		}
		throw new NonSuccessApiResponseException(response.StatusCode);
	}

	public async Task<CourseTemplateDetails> GetCourseTemplateAsync(Guid id, CancellationToken token)
	{
		using var activity = _activitySource.StartActivity();
		var modelerUri = options.Value.ModelerUri;
		return await GetAsync<CourseTemplateDetails>(client, modelerUri + $"api/CourseTemplates/{id}", token);

	}
}

