using Concrete.Core.Questions.Instances;

namespace Concrete.Quizes.Questions.Instances.MultipleChoice.Grading;

public record AllOrNothingGrading(params int[] CorrectAnswers) : IGrading
{
	QuestionGradingResponse IGrading.Grade(int[] AnswerIndicies) => new(
		AnswerIndicies.Length == CorrectAnswers.Length && AnswerIndicies.All(CorrectAnswers.Contains)
		? QuestionGradingResponse.MaxGrade
		: 0
	);
}

public record ProportionalGrading(params int[] CorrectAnswers) : IGrading
{
	QuestionGradingResponse IGrading.Grade(int[] AnswerIndicies) => new(CorrectAnswers.Where(CorrectAnswers.Contains).Count() / CorrectAnswers.Length * QuestionGradingResponse.MaxGrade);
}
