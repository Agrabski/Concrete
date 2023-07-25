namespace Concrete.Core.Questions.Templates;
public record struct QuestionTemplateInstance(Guid Id, Dictionary<string, object> Parameters, Guid QuestionTemplateId, Guid QuestionBankId);
