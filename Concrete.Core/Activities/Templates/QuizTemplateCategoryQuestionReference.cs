using Concrete.Core.Questions.Templates;

namespace Concrete.Core.Activities.Templates;

public record QuizTemplateCategoryQuestionReference(Guid QuestionBankId, CategoryName CategoryName) : IQuizTemplateQuestionReference
{
	public IQuestionTemplateFilingMode FilingMode => new AllQuestionVariantsTemplateFilingMode();

	public IQuestionTemplate? FindTemplate(IQuestionBank bank)
	{
		var questions = bank.GetQuestionsByCategory(CategoryName).ToArray();
		return questions[Random.Shared.Next(0, questions.Length)];

	}
}
