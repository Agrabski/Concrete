namespace Concrete.Core.Questions;
public class MultipleChoiceQuestion : IQuestion<MultipleChoiceQuestionChoice[]>
{
	public required Guid TemplateId { get; init; }
	public required Guid VariantId { get; init; }
	public required string Question { get; init; }
	public required List<MultipleChoiceQuestionChoice> Choices { get; init; }
	public required System.Linq.Expressions.lamb<Func<IEnumerable<MultipleChoiceQuestionChoice>, float>> ScoringExpression { get; init; }

	public float Score(MultipleChoiceQuestionChoice[] answer)
	{
		return ScoringExpression.Compile().Invoke(answer);
	}
}
