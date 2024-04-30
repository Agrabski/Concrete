namespace Concrete.Serialization;

internal class Serializer<TDataInterface, TDiscriminator>(
	IEnumerable<IDataDeserializer<TDataInterface, TDiscriminator>> deserializers
	) : ISerializer<TDataInterface, TDiscriminator> where TDiscriminator : IEquatable<TDiscriminator>
{
	public TDataInterface? Deserialize(IPolymorphicDataHolder<TDiscriminator> template)
	{
		var deserializer = deserializers.First(d => d.SupportedDiscriminator.Equals(template.Discriminator));
		return deserializer.Deserialize(template.Data);
	}
	public void Update(IPolymorphicDataHolder<TDiscriminator> template, TDataInterface data)
	{
		var deserializer = deserializers.First(d => d.SupportedDiscriminator.Equals(template.Discriminator));
		template.Data = deserializer.Serialize(data);
	}
}
