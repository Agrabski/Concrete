namespace Concrete.Core.Template;

public class CourseTemplate
{
	public required string Name { get; set; }
	public Guid Id { get; init; } = Guid.NewGuid();
	public List<ClassTemplate> ClassTemplates { get; init; } = [];
}
