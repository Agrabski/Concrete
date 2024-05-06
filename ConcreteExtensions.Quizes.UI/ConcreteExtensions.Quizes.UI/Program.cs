using Concrete.Core.Data.Api.Client;
using Concrete.CrossOriginFrameConfiguration;
using ConcreteExtensions.Quizes.UI.Client.Pages;
using ConcreteExtensions.Quizes.UI.Components;
using Concrete.Extensions.Quizes.Questions.Client;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();
builder.Services
	.AddMemoryCache(e => e.TrackStatistics = true)
	.AddQuestionsClient(builder.Configuration.GetSection("QuestionsClient").Bind)
	.AddDataApiClient(builder.Configuration.GetSection("Data").Bind)
	.AddCrossOriginFrameConfiguration(builder.Configuration.GetSection("CrossOrigin").Bind)
	.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app
	.ConfigureFrameOriginPolicy()
	.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(ConcreteExtensions.Quizes.UI.Client._Imports).Assembly);

app.Run();
