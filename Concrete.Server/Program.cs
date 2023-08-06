using Concrete.Core.Services;
using Concrete.Server;
using Concrete.Storage.EfCore;
using Concrete.Users;
using Microsoft.AspNetCore.Identity;

var app = HostHelper.BuildHost(args);

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

