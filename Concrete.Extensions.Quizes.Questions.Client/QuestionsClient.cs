
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Concrete.Extensions.Quizes.Questions.Client;

internal class QuestionsClient(
	HttpClient client,
	IOptions<QuestionsClientConfiguration> options,
	IMemoryCache cache
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
			return await options
					.Value
					.ExtensionUris
					.Select(u => client.GetFromJsonAsAsyncEnumerable<QuestionTypeName>(new Uri(u, "api/extension/question-types")).Select(r => (u, r)))
					.ToAsyncEnumerable()
					.SelectMany(e => e)
					.ToDictionaryAsync(kv => kv.r, kv => kv.u);
		}) ?? throw new Exception();
	}

	public async Task<JsonDocument> CreateQuestionTypeAsync(QuestionTypeName type, CancellationToken token)
	{
		var typeNames = await GetTypeNamesByUriAsync();
		var uri = typeNames[type];
		return await client.GetFromJsonAsync<JsonDocument>(new Uri(uri, $"api/extension/{type}"), token)
			?? throw new Exception();
	}
}
