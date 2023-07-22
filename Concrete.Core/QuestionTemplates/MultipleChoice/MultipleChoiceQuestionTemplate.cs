using Concrete.Core.Questions;
using System.Linq.Expressions;

namespace Concrete.Core.QuestionTemplates.MultipleChoice;
public class MultipleChoiceQuestionTemplate : IQuestionTemplate
{
	public LocalizedTextTemplate QuestionTemplate { get; init; } = new();
	public List<MultipleChoiceQuestionChoiceTemplate> Choices { get; init; } = new();
	public Guid TemplateId { get; init; } = Guid.NewGuid();
	public List<QuestionVariant> Variants { get; init; } = new();
	public required Expression<Func<IEnumerable<MultipleChoiceQuestionChoice>, float>> ScoringExpression { get; init; }

	public IQuestion FillTemplate(string localeId, QuestionVariant variant)
	{
		return new MultipleChoiceQuestion
		{
			TemplateId = TemplateId,
			VariantId = variant.VariantId,
			Choices = Choices.Select(c => new MultipleChoiceQuestionChoice
			{
				Text = c.TextTemplate.FillForLocale(localeId, variant.Parameters),
				IsCorrect = c.IsCorrect
			}).ToList(),
			Question = QuestionTemplate.FillForLocale(localeId, variant.Parameters),
			ScoringExpression = ScoringExpression
		};
	}
}
