using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Concrete.Core.Serialization;
internal class ConcreteSerializer : IConcreteSerializer
{
	private readonly IJsonTypeInfoResolver _typeInfoResolver;
	private JsonSerializerOptions? _jsonSerializerOptions;
	private JsonSerializerOptions Options => _jsonSerializerOptions ??= GetOptions();

	private JsonSerializerOptions GetOptions()
	{
		return new JsonSerializerOptions()
		{
			TypeInfoResolver = _typeInfoResolver,
			WriteIndented = true,
			UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
			PropertyNameCaseInsensitive = true,
			AllowTrailingCommas = true
		};
	}

	public ConcreteSerializer(IJsonTypeInfoResolver typeInfoResolver)
	{
		_typeInfoResolver = typeInfoResolver;
	}

	public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, Options);
	public T Deserialize<T>([StringSyntax(StringSyntaxAttribute.Json)] string json) => JsonSerializer.Deserialize<T>(json, Options) ?? throw new Exception();
}
