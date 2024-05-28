using Concrete.Core;

namespace Concrete.Extensions.Quizes.Questions.Core.Data;

public class MultipleChoiceQuestion : ICoreQuestion
{
	public static QuestionTypeName TypeName => new(new("Concrete", "Quizes", "Questions", "Core"), "MultipleChoice");
	public LocalisedText Question { get; set; } = new();
}
