using Concrete.Core.Questions;

namespace Concrete.Core.QuestionTemplates;

public interface IQuestionTemplate
{
	IQuestion FillTemplate(string localeId, QuestionVariant variant);
	List<QuestionVariant> Variants { get; }
}
