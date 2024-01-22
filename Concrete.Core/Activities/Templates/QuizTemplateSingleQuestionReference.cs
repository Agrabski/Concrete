using Concrete.Core.Questions.Templates;

namespace Concrete.Core.Activities.Templates;

public record QuizTemplateSingleQuestionReference(Guid QuestionBankId, Guid QuestionTemplateId, IQuestionTemplateFilingMode FilingMode) : IQuizTemplateQuestionReference
{
	public IQuestionTemplate? FindTemplate(QuestionBank bank) => bank.TryGetQuestionTemplate(QuestionTemplateId);
}
