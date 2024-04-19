using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Modeler.Extension.Client;

public static class DIExtension
{
	public static IServiceCollection AddModelerExtenionsClient(this IServiceCollection services, Action<ModelerExtensionOptions> configure)
	{
		return services
			.AddOptionsWithValidateOnStart<ModelerExtensionOptions>()
			.Configure(configure)
			.Services
			;
	}
}
