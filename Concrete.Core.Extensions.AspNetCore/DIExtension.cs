using Concrete.Core.Extensions.AspNetCore.Auth;
using Concrete.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Core.Extensions.AspNetCore;
public static class DIExtension
{
	public static IServiceCollection ConfigureConcreteSerialization(this IServiceCollection collection) =>
		collection.ConfigureOptions<JsonOptionsConfiguration>();

	public static WebApplicationBuilder ConfigureConcreteAuthentication(
		this WebApplicationBuilder builder,
		Action<IdentityOptions> configureIdentity)
	{
		builder
			.Services
			.AddTransient<PasswordHasher<IAuthenticatedUser>>()
			;
		builder
			.Services
			.AddIdentity<IAuthenticatedUser, UserRole>(configureIdentity)
			.AddRoles<UserRole>()
			.AddRoleStore<RoleStore>()
			.AddUserStore<UserStore>()
			;
		return builder;
	}
}
