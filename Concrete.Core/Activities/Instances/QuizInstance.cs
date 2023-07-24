using Concrete.Core.Questions.Templates;

namespace Concrete.Core.Activities.Instances;
public class QuizInstance : IActivity
{
	public Guid Id { get; init; }
	public Guid UserId { get; init; }
	public List<QuestionTemplateInstance> Questions { get; init; } = new();
}
