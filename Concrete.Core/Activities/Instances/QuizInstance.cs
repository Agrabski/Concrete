using Concrete.Core.Questions.Templates;

namespace Concrete.Core.Activities.Instances;
internal class QuizInstance
{
	public Guid Id { get; init; }
	public Guid TemplateId { get; init; }
	public Guid UserId { get; init; }
	public List<QuestionTemplateInstance> Questions { get; init; } = new();
}
