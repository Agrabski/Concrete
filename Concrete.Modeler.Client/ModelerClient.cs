﻿using Concrete.Core.Template;
using Concrete.Interface;
using Concrete.Interface.Templates;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Concrete.Modeler.Client;

internal class ModelerClient(
	HttpClient client,
	IDistributedCache cache) : IModelerClient
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

		var response = await client.GetAsync("api/activities", token);
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
		var response = await client.GetAsync("api/CourseTemplates", token);
		return await ReadResponse<CourseTemplateHeader[]>(response, token);
	}


	public async Task<CourseTemplateHeader> CreateCourseTemplateAsync(CancellationToken token)
	{
		using var _ = _activitySource.StartActivity();
		var response = await client.PostAsync("api/CourseTemplates/", null, token);
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
		var response = await client.GetAsync($"api/CourseTemplates/{id}", token);
		return await ReadResponse<CourseTemplateDetails>(response, token);
	}

	public Task UpdateCourseTemplateNameAsync(Guid id, string name, CancellationToken token) => throw new NotImplementedException();
	
	public async Task<ClassTemplateHeader> CreateCourseClassTemplateAsync(Guid courseTemplateId, string name, CancellationToken token)
	{
		using var activity = _activitySource.StartActivity();
		using var content = JsonContent.Create(name);
		var response = await client.PutAsync(
			$"api/CourseTemplates/{courseTemplateId}",
			content,
			token
		);
		return await ReadResponse<ClassTemplateHeader>(response, token);
	}

	public async Task<ClassTemplateDetails> GetClassTemplateAsync(Guid id, CancellationToken token)
	{
		using var activity = _activitySource.StartActivity();
		var response = await client.GetAsync( $"api/ClassTemplates/{id}", token);
		return await ReadResponse<ClassTemplateDetails>(response, token);
	}

	public async Task<Uri> GetExtensionEditorForActivityTypeAsync(ActivityTypeName name, CancellationToken token)
	{
		var cacheKey = $"Modeler-extension-activity-editor-url-{name}";
		if(await cache.GetStringAsync(cacheKey, token) is string url)
			return new Uri(url);

		var response = await client.GetAsync($"api/activities/editor/{name}", token);
		var result = new Uri(await ReadResponse<string>(response, token));
		await cache.SetStringAsync(cacheKey, result.ToString(), token);
		return result;

	}


	private async Task<T> ReadResponse<T>(HttpResponseMessage response, CancellationToken token)
	{
		using var activity = _activitySource.StartActivity();
		if (response.IsSuccessStatusCode)
		{
			activity?.AddTag("Result", "Success");
			var stream = response.Content.ReadAsStream(token);
			return await JsonSerializer.DeserializeAsync<T>(
				stream,
				_options,
				cancellationToken: token
			) ?? throw new InvalidOperationException("Response did not contain data");
		}
		activity?.AddTag("Result", response.StatusCode.ToString());
		throw new NonSuccessApiResponseException(response.StatusCode);
	}

}

