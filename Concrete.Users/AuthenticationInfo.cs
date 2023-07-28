namespace Concrete.Users;

public class AuthenticationInfo
{
	public required string Email { get; set; }
	public string? PasswordHash { get; set; }
}
