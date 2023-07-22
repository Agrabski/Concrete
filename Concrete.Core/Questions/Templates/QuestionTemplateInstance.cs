namespace Concrete.Core.Questions.Templates;
public record struct QuestionTemplateInstance(Dictionary<string, object> Parameters, Guid QuestionTemplateId);
