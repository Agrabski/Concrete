using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Users;

public static class DIExtension
{
	public static IServiceCollection AddConcreteUsers<TUserRepository>(this IServiceCollection services) where TUserRepository : class, IUserRepository =>
		services.AddScoped<IUserRepository, TUserRepository>();
}
