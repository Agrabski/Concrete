using Concrete.Extensions.Quizes.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions<QuizesConfiguration>()
	.Configure(builder.Configuration.GetSection("Quiz").Bind)
	; 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("test", () => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
