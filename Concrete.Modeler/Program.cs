using Concrete.Core.Data;
using Concrete.Modeler.Extension.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Concrete.Modeler.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddSqlServerDbContext<ConcreteContext>(
	"Concrete",
	settings =>
	{
		settings.Tracing = true;
		settings.HealthChecks = true;
	},
	builder =>
	{
		builder.EnableDetailedErrors();
		builder.EnableSensitiveDataLogging();
	}
);
builder
	.Services
	.AddModelerExtenionsClient(builder.Configuration.GetSection("Extensions").Bind);


var app = builder.Build();
app.MapGet("test", () => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app
	.Services
	.GetRequiredService<ILoggerFactory>()
	.CreateLogger("Application")
	.LogDebug(
		"Loaded configuration: {Configuration}",
		builder.Configuration.GetDebugView()
	);
using var scope = app.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<ConcreteContext>().Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
