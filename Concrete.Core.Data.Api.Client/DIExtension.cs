using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Concrete.Core.Data.Api.Client;

public static class DIExtension
{
	public static IServiceCollection AddDataApiClient(this IServiceCollection services, Action<DataClientConfiguration> configure)
	{
		return services
			.AddOptions<DataClientConfiguration>()
			.Configure(configure)
			.Services
			.AddHttpClient<IDataClient, DataClient>((sp, c) =>
			{
				var o = sp.GetRequiredService<IOptions<DataClientConfiguration>>();
				c.BaseAddress = o.Value.DataApiUri;
			})
			.UseServiceDiscovery()
			.Services
			.AddTransient<IDataClient, DataClient>()
			;
	}
}
