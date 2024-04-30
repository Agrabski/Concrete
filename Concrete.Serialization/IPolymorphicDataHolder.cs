using System.Text.Json;

namespace Concrete.Serialization;

public interface IPolymorphicDataHolder<TDiscriminator>
{
	public TDiscriminator Discriminator { get; }
	public JsonElement Data { get; set; }
}
