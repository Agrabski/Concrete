namespace Concrete.Core;

public class QuestionTemplateReference
{
	public required Guid QuestionId { get; set; }
	public required float PositiveScore { get; set; }
	public required float NegativeScore { get; set; }
	public required float NoAnwserScore { get; set; }
}
