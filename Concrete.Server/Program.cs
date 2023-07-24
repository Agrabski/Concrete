using Concrete.Core;
using Concrete.Core.Extensions.AspNetCore;
using Concrete.Quizes.Questions;
using Concrete.Storage.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddConcrete()
	.AddConcreteEfCoreStorage(o => o.UseSqlite(builder.Configuration.GetConnectionString("sqlite")))
	.AddBuiltInConcreteQuestions()
	.ConfigureConcreteSerialization()
	.AddControllers()
	;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger()
		.UseSwaggerUI();
	using var scope = app.Services.CreateScope();
	await scope.ServiceProvider.GetRequiredService<IConcreteMigrator>().EnsureCreatedAsync(CancellationToken.None);
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();


