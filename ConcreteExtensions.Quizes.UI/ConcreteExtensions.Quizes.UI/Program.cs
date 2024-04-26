using Concrete.CrossOriginFrameConfiguration;
using ConcreteExtensions.Quizes.UI.Client.Pages;
using ConcreteExtensions.Quizes.UI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
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
