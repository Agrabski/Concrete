using Concrete.Extensions.Quizes.Questions;
using Concrete.Extensions.Quizes.Questions.Core.Data;
using Concrete.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services
	.AddConcreteSerialization<ICoreQuestion, QuestionTypeName>()
	.AddSerializableType<ICoreQuestion, QuestionTypeName, MultipleChoiceQuestion>(MultipleChoiceQuestion.TypeName)
	;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.MapControllers();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();