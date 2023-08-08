using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Concrete.Core.Serialization;
internal class ConcreteSerializer : IConcreteSerializer
{
	private readonly IEnumerable<IPolymorphicTypeInfo> _typeInfos;
	private JsonSerializerOptions? _jsonSerializerOptions;
	private JsonSerializerOptions Options => _jsonSerializerOptions ??= GetOptions();

	private JsonSerializerOptions GetOptions()
	{
		return new JsonSerializerOptions()
		{
			TypeInfoResolver = new PolymorphicTypeResolver(_typeInfos),
			WriteIndented = true,
			UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
			PropertyNameCaseInsensitive = true,
			AllowTrailingCommas = true
		};
	}

	public ConcreteSerializer(IEnumerable<IPolymorphicTypeInfo> typeInfos)
	{
		_typeInfos = typeInfos;
	}

	public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, Options);
	public T Deserialize<T>([StringSyntax(StringSyntaxAttribute.Json)] string json) => JsonSerializer.Deserialize<T>(json, Options) ?? throw new Exception();
}
