namespace Concrete.Extensions.Quizes.Questions.Core.Data;

public interface ICoreQuestion
{
}

public class MultipleChoiceQuestion : ICoreQuestion
{
	public static QuestionTypeName TypeName { get; } = new(new("Concrete", "Quizes", "Questions", "Core"), "MultipleChoice");
}
