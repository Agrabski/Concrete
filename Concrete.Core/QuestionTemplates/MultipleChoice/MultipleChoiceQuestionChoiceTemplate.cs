using Concrete.Core.Questions;

namespace Concrete.Core.QuestionTemplates.MultipleChoice;
public class MultipleChoiceQuestionChoiceTemplate
{
	public LocalizedTextTemplate TextTemplate { get; init; } = new();
	public bool IsCorrect { get; init; }

	public MultipleChoiceQuestionChoice FillTemplate(string localeId, object[] parameters)
	{
		return new()
		{
			IsCorrect = IsCorrect,
			Text = TextTemplate.FillForLocale(localeId, parameters)
		};
	}
}
