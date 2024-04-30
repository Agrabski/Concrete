namespace Concrete.Serialization;

public interface ISerializer<TDataInterface, TDiscriminator>
{
	public TDataInterface? Deserialize(IPolymorphicDataHolder<TDiscriminator> holder);
	public void Update(IPolymorphicDataHolder<TDiscriminator>  template, TDataInterface data);
}
