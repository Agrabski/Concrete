using System.Text.Json;

namespace Concrete.Serialization;

internal interface IDataDeserializer<TDataInterface, TDiscriminator>
{
	public TDiscriminator SupportedDiscriminator { get; }
	public TDataInterface? Deserialize(JsonElement data);
	JsonElement Serialize(TDataInterface data);
}
