using Concrete.Core.Questions.Templates;

namespace Concrete.Core.Activities.Templates;

public interface IQuestionTemplateFilingMode
{
	QuestionTemplateInstance Fill(IQuestionTemplate template);
}
