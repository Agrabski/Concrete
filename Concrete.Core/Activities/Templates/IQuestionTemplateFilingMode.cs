using Concrete.Core.Questions.Templates;
using System.Text.Json.Serialization;

namespace Concrete.Core.Activities.Templates;

[JsonPolymorphic]
[JsonDerivedType(typeof(SpecificQuestionVariantsTemplateFilingMode), 0)]
[JsonDerivedType(typeof(AllQuestionVariantsTemplateFilingMode), 1)]
public interface IQuestionTemplateFilingMode
{
	QuestionTemplateInstance Fill(IQuestionTemplate template);
}
