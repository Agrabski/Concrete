using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Core.Extensions.AspNetCore;
public static class DIExtension
{
	public static IServiceCollection ConfigureConcreteSerialization(this IServiceCollection collection) =>
		collection.ConfigureOptions<JsonOptionsConfiguration>();
}
