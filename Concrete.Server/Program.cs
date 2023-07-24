using Concrete.Core;
using Concrete.Quizes.Questions;
using Concrete.Storage.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

var builder = WebApplication.CreateBuilder(args);


builder.Services
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddConcrete()
	.AddConcreteEfCoreStorage(o => o.UseSqlite())
	.AddBuiltInConcreteQuestions()
	.ConfigureOptions<JsonOptionsConfiguration>()
	.AddControllers()
	;
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


//todo: move to some concrete library
internal class JsonOptionsConfiguration : IConfigureOptions<JsonOptions>
{
	private readonly DefaultJsonTypeInfoResolver _resolver;

	public JsonOptionsConfiguration(DefaultJsonTypeInfoResolver resolver)
	{
		_resolver = resolver;
	}

	public void Configure(JsonOptions options)
	{
		options.JsonSerializerOptions.TypeInfoResolver = _resolver;
		options.JsonSerializerOptions.WriteIndented = true;
		options.JsonSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;

	}
}
