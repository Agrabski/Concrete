using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Serialization;


public static class DIExtension
{
	public static IServiceCollection AddConcreteSerialization<TDataInterface,TDiscriminator>(this IServiceCollection services) where TDiscriminator : IEquatable<TDiscriminator>
	{
		return services
			.AddScoped<ISerializer<TDataInterface, TDiscriminator>, Serializer<TDataInterface, TDiscriminator>>();
	}

	public static IServiceCollection AddSerializableType<TDataInterface, TDiscriminator, TConcrete>(this IServiceCollection services, TDiscriminator discriminator) where TConcrete : TDataInterface
	{
		return services.AddSingleton<IDataDeserializer<TDataInterface, TDiscriminator>>(new DataDeserializer<TDataInterface, TDiscriminator, TConcrete>()
		{ 
			SupportedDiscriminator = discriminator
		});
	}
}
