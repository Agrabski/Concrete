namespace Concrete.Users;

public interface IAuthenticatedUser
{
	string UserName { get; }
	Guid Id { get; }
	UserRole Role { get; }

	AuthenticationInfo GetAuthentication();
}
