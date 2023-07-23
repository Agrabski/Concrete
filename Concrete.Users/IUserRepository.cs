namespace Concrete.Users;

public interface IUserRepository
{
	Task<User?> TryFindUserAsync(Guid userId, CancellationToken token);
}
