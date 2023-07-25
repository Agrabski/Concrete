using Concrete.Core.Questions.CultureFilledDtos;

namespace Concrete.Core.Activities.CultureFilledDtos;
public class CultureFilledQuiz
{
	public Guid InstanceId { get; init; }
	public required ICultureFilledQuestion[] Questions { get; init; }
}
