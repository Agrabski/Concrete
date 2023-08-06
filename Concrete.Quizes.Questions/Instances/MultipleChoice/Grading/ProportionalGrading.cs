using Concrete.Core.Questions.Instances;

namespace Concrete.Quizes.Questions.Instances.MultipleChoice.Grading;

public record ProportionalGrading(params int[] CorrectAnswers) : IGrading
{
	QuestionGradingResponse IGrading.Grade(int[] AnswerIndicies) => new(CorrectAnswers.Where(CorrectAnswers.Contains).Count() / CorrectAnswers.Length * QuestionGradingResponse.MaxGrade);
}
