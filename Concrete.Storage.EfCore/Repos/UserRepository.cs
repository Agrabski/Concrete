using Concrete.Users;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Storage.EfCore.Repos;
internal class UserRepository : IUserRepository
{
	private readonly ConcreteContext _context;
	public UserRepository(ConcreteContext context)
	{
		_context = context;
	}

	public async Task<User?> TryFindUserAsync(Guid userId, CancellationToken token) => await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, token);
}
