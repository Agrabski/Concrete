using Concrete.Users;

namespace Concrete.Storage.EfCore;

public class DatabaseUser : User, IAuthenticatedUser
{
	public AuthenticationInfo? AuthenticationInfo { get; set; }

	public AuthenticationInfo GetAuthentication() => AuthenticationInfo
		?? throw new InvalidOperationException("Database was not configured to supply authentication information");
}
