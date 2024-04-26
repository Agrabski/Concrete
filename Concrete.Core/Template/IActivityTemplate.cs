using Concrete.Interface;

namespace Concrete.Core.Template;

public interface IActivityTemplate<TData>
{
	public ActivityTypeName TypeName { get; }
	public Guid Id { get; init; } 
	public LocalisedText DisplayName { get; set; }
	public TData TemplateData { get; set; }
	string Name { get; set; }
}
