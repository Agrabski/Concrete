using Concrete.Extensions.Quizes.Questions;
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
	public decimal MaxScore { get; set; }
}

public class AnyQuestionWithTag : IQuizTemplateQuestionRefernce
{
	public required QuestionTag Tag { get; set; }
	public decimal MaxScore { get; set; }
}

public class SpecificQuestion : IQuizTemplateQuestionRefernce
{
	public decimal MaxScore { get; set; }
}
