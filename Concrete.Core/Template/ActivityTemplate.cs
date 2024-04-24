using Concrete.Interface;

namespace Concrete.Core.Template;

public class ActivityTemplate: IActivityTemplate<object>
{
	public required ActivityName TypeName { get; init; }
	public Guid Id { get; init; } = Guid.NewGuid();
	public required LocalisedText DisplayName { get; set; }
	public required object TemplateData { get; set; }
	public required string Name { get; set; }
}
