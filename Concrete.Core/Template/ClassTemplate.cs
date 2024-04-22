namespace Concrete.Core.Template;

public class ClassTemplate
{
	public Guid Id { get; init; } = Guid.NewGuid();
	public required string Name { get; set; }
	public List<ActivityTemplate> ActivityTemplates { get; init; } = [];
}
