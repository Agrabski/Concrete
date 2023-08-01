using Concrete.Core;
using Concrete.Core.Extensions.AspNetCore;
using Concrete.Core.Services;
using Concrete.Quizes.Questions;
using Concrete.Storage.EfCore;
using Concrete.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services
	.AddEndpointsApiExplorer()
	.AddSwaggerGen(s =>
	{
		s.SupportNonNullableReferenceTypes();
		s.SchemaFilter<RequiredNotNullableSchemaFilter>();
	})
	.AddConcrete()
	.AddConcreteEfCoreStorage(true, o => o.UseSqlite(builder.Configuration.GetConnectionString("sqlite")))
	.AddBuiltInConcreteQuestions()
	.ConfigureConcreteSerialization()
	.AddControllers()
	;
builder
	.ConfigureConcreteAuthentication(o => { });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger()
		.UseSwaggerUI();
	using var scope = app.Services.CreateScope();
	if (await scope.ServiceProvider.GetRequiredService<IConcreteMigrator>().EnsureCreatedAsync(CancellationToken.None))
	{
		var repository = scope.ServiceProvider.GetRequiredService<IAuthenticationInfoRepository>();
		var hasher = new PasswordHasher<IAuthenticatedUser>();
		var admin = new DatabaseUser()
		{
			Name = "admin",
			Surname = "admin",
			UserName = "admin",
			Role = UserRole.Admin,
			Id = Guid.NewGuid(),
			AuthenticationInfo = new AuthenticationInfo { Email = "test@test.com", PasswordHash = string.Empty }
		};
		admin.AuthenticationInfo.PasswordHash = hasher.HashPassword(admin, "change-me");
		await repository.AddAuthenticatedUserAsync(admin, CancellationToken.None);
		await scope.ServiceProvider.GetRequiredService<IConcreteUnitOfWork>().CommitAsync(CancellationToken.None);
	}
}

app.UseHttpsRedirection();

app
	// idiotic hack around the fact that asp.net core identity redirects to /Account/accessdenied instead of returning 403
	.Use(async (context, next) =>
	{
		await next(context);
		if (context.Response.StatusCode == 302)
		{
			context.Response.StatusCode = 403;
		}
	})
	.UseAuthentication()
	.UseAuthorization()
	;

app.MapControllers();

app.Run();


