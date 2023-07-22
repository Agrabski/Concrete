using Concrete.Core.Questions.Instances;
using System.Text.Json.Serialization;

namespace Concrete.Quizes.Questions.Instances.MultipleChoice.Grading;
[JsonDerivedType(typeof(AllOrNothingGrading), 0)]
[JsonDerivedType(typeof(ProportionalGrading), 1)]
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$$type")]
public interface IGrading
{
	internal QuestionGradingResponse Grade(int[] AnswerIndicies);
}
