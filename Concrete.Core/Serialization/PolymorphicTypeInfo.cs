using System.Text.Json.Serialization.Metadata;

namespace Concrete.Core.Serialization;
internal interface IPolymorphicTypeInfo
{
	JsonDerivedType? TryGetDerivedType(Type baseType);
}
internal class PolymorphicTypeInfo<TBaseType> : IPolymorphicTypeInfo
{
	public required JsonDerivedType TypeInfo { get; init; }
	public static PolymorphicTypeInfo<TBaseType>
		FromImplementation<TImplementation>(string? discriminator = null)
		where TImplementation : TBaseType
	{
		var type = typeof(TImplementation);
		return new()
		{
			TypeInfo = new JsonDerivedType(type, discriminator ?? type.FullName ?? type.Name)
		};
	}

	public JsonDerivedType? TryGetDerivedType(Type baseType)
	{
		if (baseType == typeof(TBaseType))
			return TypeInfo;
		return null;
	}
}
