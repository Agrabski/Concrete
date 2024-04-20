using System.Text.Json;
using System.Text.Json.Serialization;

namespace Concrete.Interface;

internal class ParsableJsonConverter<T> : JsonConverter<T> where T : IParsable<T>
{
	public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (T.TryParse(reader.GetString(), null, out var result))
			return result;
		return default;
	}
	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}
