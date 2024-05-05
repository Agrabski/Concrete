using System.Text.Json.Serialization;

namespace Concrete.Extensions.Quizes;

[JsonPolymorphic]
[JsonDerivedType(typeof(AnyQuestionWithTag), "any")]
[JsonDerivedType(typeof(SpecificQuestion), "specific")]
public interface IQuizTemplateQuestionRefernce
{
	public decimal MaxScore { get; set; }
}
