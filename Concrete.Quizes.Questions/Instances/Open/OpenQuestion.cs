using Concrete.Core.Questions.CultureFilledDtos;
using Concrete.Core.Questions.Instances;
using Concrete.Localization;
using Concrete.Quizes.Questions.CultureFilledDtos.Open;

namespace Concrete.Quizes.Questions.Instances.Open;

public class OpenQuestion : IQuestion<OpenQuestionAnswer>
{
	public required LocalisedString Question { get; init; }
	public required Guid QuestionId { get; init; }
	public required Guid TemplateId { get; init; }
	public required Dictionary<string, object> Parameters { get; init; }

	public ICultureFilledQuestion FllForCulture(string culture) => new CultureFilledOpenQuestionDto(Question.GetTextForLocale(culture), QuestionId);
	public IQuestionGradingResponse Grade(OpenQuestionAnswer answer) => new ManualGradingResponse<OpenQuestionAnswer>(answer, null);
}
