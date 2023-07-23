using Concrete.Core.Questions.CultureFilledDtos;

namespace Concrete.Core.Questions.Instances;

public interface IQuestion
{
	Guid QuestionId { get; }
	Guid TemplateId { get; }
	Dictionary<string, object> Parameters { get; }
	ICultureFilledQuestion FllForCulture(string culture);
}
public interface IQuestion<TAnswer> : IQuestion where TAnswer : IQuestionAnswer
{
	public QuestionGradingResponse Grade(TAnswer answer);
}
