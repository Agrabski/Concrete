using System.Text.Json;

namespace Concrete.Extensions.Quizes.Questions.Client;

public interface IQuestionExtenionsClient
{
	Task<JsonDocument> CreateQuestionTypeAsync(QuestionTypeName type, CancellationToken token);
	public Task<QuestionTypeName[]> GetAllAvailableQuestionTypesAsync(CancellationToken token);
}
