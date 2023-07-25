using Concrete.Core.Activities.CultureFilledDtos;
using Concrete.Core.Activities.Instances;
using Concrete.Core.Activities.Templates;
using Concrete.Core.Services.QuestionBanks;
using Concrete.Core.Services.Subjects;

namespace Concrete.Core.Services.Activities;

internal class QuizService : IQuizService
{
	private readonly IActivityInstanceRepository _activityInstanceRepository;
	private readonly ISubjectRepository _subjectRepository;
	private readonly IQuestionBankRepository _questionBankRepo;

	public QuizService(
		ISubjectRepository subjectRepository,
		IQuestionBankRepository questionBankRepo,
		IActivityInstanceRepository activityInstanceRepository)
	{
		_subjectRepository = subjectRepository;
		_questionBankRepo = questionBankRepo;
		_activityInstanceRepository = activityInstanceRepository;
	}

	public async Task<CultureFilledQuiz> GetQuizAsync(Guid quizInstanceId, string locale, CancellationToken cancellationToken)
	{
		var activity = await _activityInstanceRepository.TryFindAsync<QuizInstance>(quizInstanceId, cancellationToken)
			?? throw new Exception($"Quiz instance with id {quizInstanceId} was not found");
		return await activity.FillForCulture(locale, _questionBankRepo, cancellationToken);
	}
	public async Task<Guid> StartQuizAttempt(Guid userId, Guid courseId, Guid subjectId, Guid activityId, CancellationToken token)
	{
		var subject = await _subjectRepository.TryGetSubjectAsync(courseId, subjectId, token)
			?? throw new Exception("Subject not found");
		var activity = subject.Activities.FirstOrDefault(a => a.Id == activityId)
			?? throw new Exception("Activity not found");
		if (activity.Template is QuizTemplate quiz)
		{
			var instance = await quiz.CreateInstanceAsync(userId, _questionBankRepo, token);
			await _activityInstanceRepository.AddAsync(instance, token);
			return instance.InstanceId;
		}
		throw new InvalidOperationException($"Activity {activityId} in subject {subjectId} is not a quiz. It is a {activity.Template.GetType()}");

	}
}
