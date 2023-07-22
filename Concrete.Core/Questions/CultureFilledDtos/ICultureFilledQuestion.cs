namespace Concrete.Core.Questions.CultureFilledDtos;
public interface ICultureFilledQuestion
{
	Guid TemplateId { get; }
	Dictionary<string, object> Parameters { get; }
}
