using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Modeler.Client;

public static class DIExtension
{
	public static IServiceCollection AddModelerClient(this IServiceCollection services, Action<ModelerClientOptions> configure)
	{
		return services
			.AddSingleton<IModelerClient, ModelerClient>()
			.AddOptionsWithValidateOnStart<ModelerClientOptions>()
			.Configure(configure)
			.Services
			.AddHttpClient<ModelerClient>(_ => { })
			.AddDefaultLogger()
			.UseServiceDiscovery()
			.Services
			;
	}
}

