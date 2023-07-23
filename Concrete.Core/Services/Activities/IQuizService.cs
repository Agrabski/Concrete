using Concrete.Core.Activities.CultureFilledDtos;
using Concrete.Core.Activities.Templates;
using Concrete.Core.Services.QuestionBanks;
using Concrete.Core.Services.Subjects;

namespace Concrete.Core.Services.Activities;
public interface IQuizService
{
	Task<CultureFilledQuiz> GetQuizAsync(Guid QuizInstanceId, string locale, CancellationToken cancellationToken);
	Task<Guid> StartQuizAttempt(Guid userId, Guid courseId, Guid subjectId, Guid activityId, CancellationToken token);
}

internal class QuizService : IQuizService
{
	private readonly ISubjectRepository _subjectRepository;
	private readonly IQuestionBankRepository _questionBankRepo;

	public QuizService(ISubjectRepository subjectRepository, IQuestionBankRepository questionBankRepo)
	{
		_subjectRepository = subjectRepository;
		_questionBankRepo = questionBankRepo;
	}

	public Task<CultureFilledQuiz> GetQuizAsync(Guid QuizInstanceId, string locale, CancellationToken cancellationToken) => throw new NotImplementedException();
	public async Task<Guid> StartQuizAttempt(Guid userId, Guid courseId, Guid subjectId, Guid activityId, CancellationToken token)
	{
		var subject = await _subjectRepository.TryGetSubjectAsync(courseId, subjectId, token)
			?? throw new Exception("Subject not found");
		var activity = subject.Activities.FirstOrDefault(a => a.Id == activityId)
			?? throw new Exception("Activity not found");
		if (activity.Template is QuizTemplate quiz)
		{
			var instance = await quiz.CreateInstanceAsync(userId, _questionBankRepo, token);
			// todo: store instance somwhere
			return instance.Id;
		}
		throw new InvalidOperationException($"Activity {activityId} in subject {subjectId} is not a quiz. It is a {activity.Template.GetType()}");

	}
}
