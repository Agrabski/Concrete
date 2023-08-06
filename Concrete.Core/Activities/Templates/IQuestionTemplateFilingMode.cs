using Concrete.Core.Questions.Templates;
using System.Text.Json.Serialization;

namespace Concrete.Core.Activities.Templates;

[JsonPolymorphic]
[JsonDerivedType(typeof(SpecificQuestionVariantsTemplateFilingMode), "specific")]
[JsonDerivedType(typeof(AllQuestionVariantsTemplateFilingMode), "all")]
public interface IQuestionTemplateFilingMode
{
	QuestionTemplateInstance Fill(IQuestionTemplate template);
}
