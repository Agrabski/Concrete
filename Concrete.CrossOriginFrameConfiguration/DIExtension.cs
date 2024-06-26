﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Concrete.CrossOriginFrameConfiguration;

public static class DIExtension
{
	public static IServiceCollection AddCrossOriginFrameConfiguration(this IServiceCollection services, Action<CrossOriginConfig> configure) =>
		services
		.AddOptions<CrossOriginConfig>()
		.Configure(configure)
		.Services
		;

	public static WebApplication ConfigureFrameOriginPolicy(this WebApplication app)
	{
		app.Use((context, next) =>
		{
			context.Response.OnStarting(() =>
			{
				var options = context.RequestServices.GetRequiredService<IOptions<CrossOriginConfig>>();
				var looger = context.RequestServices.GetRequiredService<ILogger<CrossOriginConfig>>();
				var csp = context.Response.Headers.ContentSecurityPolicy;
				var value = $"frame-ancestors {string.Join(' ', options.Value.AllowedUrls)}";
				looger.LogDebug("Setting frame ancestor csp {CSP}", value);
				var additionalCspDirectives = options.Value.AdditionalCspDirectives;
				looger.LogDebug("Setting additional csp directives: {Csp Directives Count}", additionalCspDirectives.Count);
				context.Response.Headers.ContentSecurityPolicy = new StringValues([value, .. additionalCspDirectives]);
				return Task.CompletedTask;
			});
			return next();
		});
		return app;
	}
}
