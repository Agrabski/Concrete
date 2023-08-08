using Concrete.Core.Questions.Instances;
using System.Text.Json.Serialization;

namespace Concrete.Quizes.Questions.Instances.MultipleChoice.Grading;
[JsonDerivedType(typeof(AllOrNothingGrading), "all-or-nothing")]
[JsonDerivedType(typeof(ProportionalGrading), "proportional")]
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$$type")]
public interface IGrading
{
	internal IQuestionGradingResponse Grade(int[] AnswerIndicies);
}
