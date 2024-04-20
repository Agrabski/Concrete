namespace Concrete.Core.Template;

public class ClassTemplate
{
	public Guid Id { get; init; } = Guid.NewGuid();
	public List<ActivityTemplate> ActivityTemplates { get; init; } = [];
}
