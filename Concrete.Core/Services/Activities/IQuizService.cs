using Concrete.Core.Activities.CultureFilledDtos;

namespace Concrete.Core.Services.Activities;
public interface IQuizService
{
	Task<CultureFilledQuiz> GetQuizAsync(Guid QuizInstanceId, string locale, CancellationToken cancellationToken);
	Task<Guid> StartQuizAttempt(Guid userId, Guid courseId, Guid subjectId, Guid activityId, CancellationToken token);
}
