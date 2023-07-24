using Concrete.Core;
using Concrete.Quizes.Questions;
using Concrete.Storage.EfCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

var builder = WebApplication.CreateBuilder(args);


builder.Services
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddConcrete()
	.AddConcreteEfCoreStorage(o => o.UseSqlite())
	.AddBuiltInConcreteQuestions()
	;
builder.Services
	.AddControllers()
	.AddJsonOptions(options =>
	{
#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
		// todo: this hack is beyound ugly, but it works for now
		// need to figure out how to configure type resolver in a configure action
		options.JsonSerializerOptions.TypeInfoResolver = builder
			.Services
			.BuildServiceProvider()
			.GetRequiredService<DefaultJsonTypeInfoResolver>();
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
		options.JsonSerializerOptions.WriteIndented = true;
		options.JsonSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger()
		.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
