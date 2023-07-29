namespace Concrete.Users;

public interface IAuthenticatedUser
{
	string UserName { get; set; }
	Guid Id { get; }
	UserRole Role { get; set; }

	AuthenticationInfo GetAuthentication();
}
