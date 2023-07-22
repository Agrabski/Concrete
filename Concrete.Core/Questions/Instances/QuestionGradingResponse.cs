namespace Concrete.Core.Questions.Instances;

public record struct QuestionGradingResponse(int Grade)
{
	public const int MaxGrade = 1000;
}
