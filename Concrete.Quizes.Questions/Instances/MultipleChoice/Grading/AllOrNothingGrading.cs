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
