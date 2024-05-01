using Concrete.Interface;
using Concrete.Serialization;
using System.Text.Json;

namespace Concrete.Core.Template;

public class ActivityTemplate: IPolymorphicDataHolder<ActivityTypeName>
{
	public Guid Id { get; init; } = Guid.NewGuid();
	public required LocalisedText DisplayName { get; set; }
	public required string Name { get; set; }
	public required ActivityTypeName Discriminator { get; set; }
	public required JsonDocument Data { get; set; }
}
