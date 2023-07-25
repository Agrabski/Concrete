using Concrete.Core.Activities.CultureFilledDtos;
using Concrete.Core.Questions.CultureFilledDtos;
using Concrete.Core.Questions.Templates;
using Concrete.Core.Services.QuestionBanks;

namespace Concrete.Core.Activities.Instances;
public class QuizInstance : IActivity
{
	public Guid InstanceId { get; init; }
	public Guid UserId { get; init; }
	public List<QuestionTemplateInstance> Questions { get; init; } = new();
	public async Task<CultureFilledQuiz> FillForCulture(string cultureId, IQuestionBankRepository questionBankRepository, CancellationToken token)
	{
		return new()
		{
			InstanceId = InstanceId,
			Questions = await FillQuestionsAsync(Questions, questionBankRepository, cultureId, token),
		};
	}

	private async Task<ICultureFilledQuestion[]> FillQuestionsAsync(List<QuestionTemplateInstance> questions, IQuestionBankRepository questionBankRepository, string culture, CancellationToken token)
	{
		var result = new List<ICultureFilledQuestion>();
		foreach (var question in questions)
		{
			var bank = await questionBankRepository.TryGet(question.QuestionBankId, token)
				?? throw new Exception();
			var template = bank.TryGetQuestionTemplate(question.QuestionTemplateId)
				?? throw new Exception();
			result.Add(template.FillQuestionTemplate(question.Parameters).FllForCulture(culture));
		}
		return result.ToArray();
	}
}
