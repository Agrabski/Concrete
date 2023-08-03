using Concrete.Core.Questions.Templates;
using System.Text.Json.Serialization;

namespace Concrete.Core.Activities.Templates;

[JsonPolymorphic]
[JsonDerivedType(typeof(QuizTemplateSingleQuestionReference), "single-question")]
[JsonDerivedType(typeof(QuizTemplateCategoryQuestionReference), "category")]
public interface IQuizTemplateQuestionReference
{
	IQuestionTemplateFilingMode FilingMode { get; }
	Guid QuestionBankId { get; }
	public IQuestionTemplate? FindTemplate(IQuestionBank bank);
}
