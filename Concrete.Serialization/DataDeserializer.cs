using System.Text.Json;

namespace Concrete.Serialization;

internal class DataDeserializer<TDataInterface, TDiscriminator, TConcrete> : IDataDeserializer<TDataInterface, TDiscriminator> where TConcrete : TDataInterface
{
	public required TDiscriminator SupportedDiscriminator { get; init; }

	public TDataInterface? Deserialize(JsonDocument data)
	{
		return JsonSerializer.Deserialize<TConcrete>(data);
	}

	public JsonDocument Serialize(TDataInterface data)
	{
		return JsonSerializer.SerializeToDocument(data);
	}
}
