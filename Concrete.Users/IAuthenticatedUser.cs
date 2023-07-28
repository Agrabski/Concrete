namespace Concrete.Users;

public interface IAuthenticatedUser
{
	string UserName { get; }
	Guid Id { get; }

	AuthenticationInfo GetAuthentication();
}
