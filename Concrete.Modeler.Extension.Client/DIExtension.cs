using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Concrete.Modeler.Extension.Client;

public static class DIExtension
{
	public static IServiceCollection AddModelerExtenionsClient(this IServiceCollection services, Action<ModelerExtensionOptions> configure)
	{
		return services
			.AddSingleton<IModelerExtensionClient, ModelerExtensionClient>()
			.AddOptionsWithValidateOnStart<ModelerExtensionOptions>()
			.Configure(configure)
			.Services
			.AddHttpClient<ModelerExtensionClient>(_ => { })
			.AddDefaultLogger()
			.UseServiceDiscovery()
			.Services
			;
	}
}

