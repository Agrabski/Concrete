using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Concrete.Modeler.Client;

public static class DIExtension
{
	public static IServiceCollection AddModelerClient(this IServiceCollection services, Action<ModelerClientOptions> configure)
	{
		return services
			.AddOptionsWithValidateOnStart<ModelerClientOptions>()
			.Configure(configure)
			.Services
			.AddTransient<IModelerClient, ModelerClient>()
			.AddHttpClient<IModelerClient, ModelerClient>((sp, client) =>
			{
				var options = sp.GetRequiredService<IOptions<ModelerClientOptions>>();
				client.BaseAddress = options.Value.ModelerUri;
				var logger = sp.GetRequiredService<ILogger<ModelerClient>>();
				logger.LogDebug("Client base address is: {BaseAddress}", client.BaseAddress);
			})
			.AddDefaultLogger()
			.UseServiceDiscovery()
			.Services
			;
	}
}

