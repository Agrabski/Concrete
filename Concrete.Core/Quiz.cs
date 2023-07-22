using Concrete.Core.Questions;

namespace Concrete.Core;

public class Quiz
{
	public List<IQuestion> Questions { get; set; } = new();
}
