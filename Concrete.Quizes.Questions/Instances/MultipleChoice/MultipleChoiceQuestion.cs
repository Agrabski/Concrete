using Concrete.Core.Questions.CultureFilledDtos;
using Concrete.Core.Questions.Instances;
using Concrete.Localization;

namespace Concrete.Quizes.Questions.Instances.MultipleChoice;

public class MultipleChoiceQuestion : IQuestion<MultipleChoiceQuestionAnswerSet>
{
	public required LocalisedString Question { get; init; }
	public List<MultipleChoiceQuestionAnswer> Answers { get; init; } = new();
	public required Guid TemplateId { get; init; }
	public required Dictionary<string, object> Parameters { get; init; }

	public ICultureFilledQuestion FllForCulture(string culture) => throw new NotImplementedException();
	public QuestionGradingResponse Grade(MultipleChoiceQuestionAnswerSet answer) => throw new NotImplementedException();
}
