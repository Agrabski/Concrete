using Concrete.Core.Data.Api.Client;
using Concrete.CrossOriginFrameConfiguration;
using Concrete.Extensions.Quizes.Questions;
using Concrete.Extensions.Quizes.Questions.Core.Components;
using Concrete.Extensions.Quizes.Questions.Core.Data;
using Concrete.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddControllers();

builder.Services
	.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddRazorPages();
builder.Services.AddCrossOriginFrameConfiguration(builder.Configuration.GetSection("CrossOrigin").Bind);
builder.Services
	.AddDataApiClient(builder.Configuration.GetSection("Data").Bind)
	.AddConcreteSerialization<ICoreQuestion, QuestionTypeName>()
	.AddSerializableType<ICoreQuestion, QuestionTypeName, MultipleChoiceQuestion>(MultipleChoiceQuestion.TypeName)
	;

builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
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
	;

//app.MapControllers();

app.Run();
