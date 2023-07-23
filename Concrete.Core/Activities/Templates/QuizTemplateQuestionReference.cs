namespace Concrete.Core.Activities.Templates;

public record QuizTemplateQuestionReference(Guid QuestionBankId,Guid QuestionTemplateId, IQuestionTemplateFilingMode FilingMode);
