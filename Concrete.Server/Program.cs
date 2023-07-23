using Concrete.Core;
using Concrete.Quizes.Questions;
using Concrete.Storage.EfCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddConcrete()
	.AddConcreteEfCoreStorage()
	.AddBuiltInConcreteQuestions();

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
