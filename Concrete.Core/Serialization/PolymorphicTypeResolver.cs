using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

[assembly: InternalsVisibleTo("Concrete.Core.Extensions.AspNetCore")]
namespace Concrete.Core.Serialization;

internal class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
{
	private readonly IPolymorphicTypeInfo[] _typeInfos;

	public PolymorphicTypeResolver(IEnumerable<IPolymorphicTypeInfo> typeInfos)
	{
		_typeInfos = typeInfos.ToArray();
	}

	public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
	{
		var jsonTypeInfo = base.GetTypeInfo(type, options);
		if (
			type.IsInterface &&
			_typeInfos
				.Select(t => t.TryGetDerivedType(type))
				.Where(t => t != null)
				.Select(t => t!.Value)
				.ToArray() is { Length: > 0 } types)
		{
			return GetOptions(jsonTypeInfo, types);
		}
		return jsonTypeInfo;
	}
	private JsonTypeInfo GetOptions(JsonTypeInfo baseInfo, IEnumerable<JsonDerivedType> derivedTypes)
	{
		baseInfo.PolymorphismOptions = new JsonPolymorphismOptions
		{
			TypeDiscriminatorPropertyName = "$$type",
			IgnoreUnrecognizedTypeDiscriminators = true,
			UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
		};
		foreach (var t in derivedTypes)
			baseInfo.PolymorphismOptions.DerivedTypes.Add(t);
		return baseInfo;
	}
}
