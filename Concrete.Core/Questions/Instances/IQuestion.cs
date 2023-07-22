using Concrete.Core.Questions.CultureFilledDtos;

namespace Concrete.Core.Questions.Instances;

public interface IQuestion
{
	Guid TemplateId { get; }
	Dictionary<string, object> Parameters { get; }
	ICultureFilledQuestion FllForCulture(string culture);
}
public interface IQuestion<TAnswer> : IQuestion
{
	public QuestionGradingResponse Grade(TAnswer answer);
}
