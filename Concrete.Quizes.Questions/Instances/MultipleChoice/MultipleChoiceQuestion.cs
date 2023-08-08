using Concrete.Core.Questions.CultureFilledDtos;
using Concrete.Core.Questions.Instances;
using Concrete.Localization;
using Concrete.Quizes.Questions.CultureFilledDtos.MultipleChoice;
using Concrete.Quizes.Questions.Instances.MultipleChoice.Grading;

namespace Concrete.Quizes.Questions.Instances.MultipleChoice;

public class MultipleChoiceQuestion : IQuestion<MultipleChoiceQuestionAnswerSet>
{
	public required LocalisedString Question { get; init; }
	public List<MultipleChoiceQuestionAnswer> Answers { get; init; } = new();
	public required Guid TemplateId { get; init; }
	public required Dictionary<string, object> Parameters { get; init; }
	public required Guid QuestionId { get; init; }
	public required IGrading Grading { get; init; }
	public List<string> Categories { get; init; } = new();

	public ICultureFilledQuestion FllForCulture(string culture) => new CultureFilledMultipleChoiceQuestionDto(
		Question.GetTextForLocale(culture),
		Answers.Select(a => new CultureFilledMultipleChoiceAnswerDto(a.Text.GetTextForLocale(culture), a.Index)).ToArray(),
		QuestionId
	);
	public IQuestionGradingResponse Grade(MultipleChoiceQuestionAnswerSet answer) => Grading.Grade(answer.AnswerIndicies);
}
