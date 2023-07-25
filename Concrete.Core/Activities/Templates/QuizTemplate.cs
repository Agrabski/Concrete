using Concrete.Core.Activities.Instances;
using Concrete.Core.Services.QuestionBanks;

namespace Concrete.Core.Activities.Templates;
public class QuizTemplate : IActivityTemplate
{
	public List<QuizTemplateQuestionReference> Questions { get; init; } = new();
	public async Task<QuizInstance> CreateInstanceAsync(
		Guid userId,
		IQuestionBankRepository questionBankRepository,
		CancellationToken token
	)
	{
		return new()
		{
			InstanceId = Guid.NewGuid(),
			UserId = userId,
			Questions = (await Task.WhenAll(Questions.Select(async q =>
			{
				var questionBank = (await questionBankRepository.TryGet(q.QuestionBankId, token))
					?? throw new Exception();//todo
				var question = questionBank.TryGetQuestionTemplate(q.QuestionTemplateId)
					?? throw new Exception(); // todo
				return q.FilingMode.Fill(question);
			}))).ToList()
		};
	}
}
