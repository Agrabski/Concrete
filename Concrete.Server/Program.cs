using Concrete.Core;
using Concrete.Quizes.Questions;
using Concrete.Storage.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddConcrete()
	.AddConcreteEfCoreStorage(o => o.UseSqlite())
	.AddBuiltInConcreteQuestions()
	.ConfigureConcreteJsonSerializerOptions()
	.AddControllers();

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
