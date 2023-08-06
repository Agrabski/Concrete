using Concrete.Core;
using Concrete.Core.Extensions.AspNetCore;
using Concrete.Quizes.Questions;
using Concrete.Storage.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Concrete.Server;

public class HostHelper
{
	public static WebApplication BuildHost(string[] args)
	{
		var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
		{
			ApplicationName = typeof(HostHelper).Namespace
		});


		builder.Services
			.AddEndpointsApiExplorer()
			.AddSwaggerGen()
			.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfiguration>()
			.AddConcrete()
			.AddConcreteEfCoreStorage(true, o => o.UseSqlite(builder.Configuration.GetConnectionString("sqlite")))
			.AddBuiltInConcreteQuestions()
			.ConfigureConcreteSerialization()
			.AddControllers()
			;
		builder
			.ConfigureConcreteAuthentication(o => { });
		var app = builder.Build();
		return app;
	}

}
