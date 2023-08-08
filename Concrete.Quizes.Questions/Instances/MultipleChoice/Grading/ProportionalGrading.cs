using Concrete.Core.Questions.Instances;

namespace Concrete.Quizes.Questions.Instances.MultipleChoice.Grading;

public record ProportionalGrading(params int[] CorrectAnswers) : IGrading
{
	IQuestionGradingResponse IGrading.Grade(int[] AnswerIndicies) => new QuestionAutomaticGradingResponse(CorrectAnswers.Where(CorrectAnswers.Contains).Count() / CorrectAnswers.Length * IQuestionGradingResponse.MaxGrade);
}
