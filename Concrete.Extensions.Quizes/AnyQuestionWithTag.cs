using Concrete.Extensions.Quizes.Questions;

namespace Concrete.Extensions.Quizes;

public class AnyQuestionWithTag : IQuizTemplateQuestionRefernce
{
	public required QuestionTag Tag { get; set; }
	public decimal MaxScore { get; set; }
}
