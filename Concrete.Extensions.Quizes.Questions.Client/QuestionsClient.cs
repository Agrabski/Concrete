
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Concrete.Extensions.Quizes.Questions.Client;

internal class QuestionsClient(
	HttpClient client,
	IOptions<QuestionsClientConfiguration> options,
	IMemoryCache cache,
	ILogger<QuestionsClient> logger
) : IQuestionExtenionsClient
{
	public async Task<QuestionTypeName[]> GetAllAvailableQuestionTypesAsync(CancellationToken token)
	{
		return (await GetTypeNamesByUriAsync()).Keys.ToArray();
	}

	private async Task<Dictionary<QuestionTypeName, Uri>> GetTypeNamesByUriAsync()
	{
		return await cache.GetOrCreateAsync("AllQuestionTypeNames", async e =>
		{
			e.SetSlidingExpiration(TimeSpan.FromMinutes(10));
			var result = await Task.WhenAll(
				options
					.Value
					.ExtensionUris
					.Select(async u =>
					{
						try
						{
							return (await client.GetFromJsonAsAsyncEnumerable<QuestionTypeName>(new Uri(u, "api/extension/question-types")).Select(r => (u, r)).ToArrayAsync());
						}
						catch (Exception ex)
						{
							logger.LogError(ex, "Calling extension {ExtensionUri} resulted in an error", u);
							throw;
						}
					})
				);
			return result.SelectMany(e => e).ToDictionary(kv => kv.r, kv => kv.u);
		}) ?? throw new Exception();
	}

	public async Task<JsonDocument> CreateQuestionTypeAsync(QuestionTypeName type, CancellationToken token)
	{
		var typeNames = await GetTypeNamesByUriAsync();
		var uri = typeNames[type];
		return await client.GetFromJsonAsync<JsonDocument>(new Uri(uri, $"api/extension/{type}"), token)
			?? throw new Exception();
	}

	public async Task<Uri> GetEditorUriAsync(QuestionTypeName questionTypeName, CancellationToken token)
	{
		var typeNames = await GetTypeNamesByUriAsync();
		var uri = typeNames[questionTypeName];
		return await client.GetFromJsonAsync<Uri>(new Uri(uri, $"api/extension/question-editor/{questionTypeName}"), token)
			?? throw new Exception();
	}
}
