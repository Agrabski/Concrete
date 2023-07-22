using System.Text.Json.Serialization.Metadata;

namespace Concrete.Core.Serialization;
internal class PolymorphicTypeInfo<TBaseType>
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
}
