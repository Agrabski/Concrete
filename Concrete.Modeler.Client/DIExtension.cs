using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
			.AddHttpClient<ModelerClient>((sp,client) => 
			{
				var options = sp.GetRequiredService<IOptions<ModelerClientOptions>> ();
				client.BaseAddress = options.Value.ModelerUri;
			})
			.AddDefaultLogger()
			.UseServiceDiscovery()
			.Services
			;
	}
}

