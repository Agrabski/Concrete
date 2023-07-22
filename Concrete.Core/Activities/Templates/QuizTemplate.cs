namespace Concrete.Core.Activities.Templates;
public class QuizTemplate : IActivityTemplate
{
	public List<QuizTemplateQuestionReference> Questions { get; init; } = new();
}
