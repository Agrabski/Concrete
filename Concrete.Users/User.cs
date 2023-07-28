namespace Concrete.Users;
public class User
{
	public Guid Id { get; init; }
	public required string UserName { get; set; }
	public required string Name { get; set; }
	public required string Surname { get; set; }
	public UserRole Role { get; set; }
}
