using Concrete.Core.Data.Api.Client;
using Concrete.Modeler.Client;
using Concrete.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisDistributedCache(
	"cache",
	s=>
	{
		s.Tracing = true;
		s.HealthChecks = true;
	}
);
builder.AddRedisOutputCache("cache");
builder.Services.AddMemoryCache();

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder
	.Services
	.AddDataApiClient(builder.Configuration.GetSection("DataClient").Bind)
	.AddModelerClient(builder.Configuration.GetSection("ModelerClient").Bind);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
