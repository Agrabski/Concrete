using Concrete.Extensions.Quizes.Questions.Template;
using System.Text.Json.Serialization;

namespace Concrete.Extensions.Quizes;

public class QuizTemplateData
{
	public List<IQuizTemplateQuestionRefernce> Questions { get; set; } = [];
}

[JsonPolymorphic]
[JsonDerivedType(typeof(AnyQuestionWithTag), "any")]
[JsonDerivedType(typeof(SpecificQuestion), "specific")]
public interface IQuizTemplateQuestionRefernce
{

}

public class AnyQuestionWithTag : IQuizTemplateQuestionRefernce
{

}

public class SpecificQuestion : IQuizTemplateQuestionRefernce
{

}
