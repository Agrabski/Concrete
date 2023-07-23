using Concrete.Users;

namespace Concrete.Core;
public class StudentGroup
{
	public required Guid Id { get; init; }
	public required string Name { get; init; }
	public List<User> Users { get; init; } = new();
}
