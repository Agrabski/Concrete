using Concrete.Core.Questions.Instances;

namespace Concrete.Quizes.Questions.Instances.MultipleChoice.Grading;

public record AllOrNothingGrading(params int[] CorrectAnswers) : IGrading
{
	IQuestionGradingResponse IGrading.Grade(int[] AnswerIndicies) => new QuestionAutomaticGradingResponse(
		AnswerIndicies.Length == CorrectAnswers.Length && AnswerIndicies.All(CorrectAnswers.Contains)
		? IQuestionGradingResponse.MaxGrade
		: 0
	);
}
