using Concrete.Users;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Storage.EfCore.Repos;
internal class UserRepository : IUserRepository, IAuthenticationInfoRepository
{
	private readonly ConcreteContext _context;
	public UserRepository(ConcreteContext context)
	{
		_context = context;
	}

	public async Task AddAuthenticatedUserAsync(IAuthenticatedUser user, CancellationToken token)
	{
		await _context.Users.AddAsync((DatabaseUser)user, token);
	}

	public async Task<AuthenticationInfo> GetAuthenticationInfoForUserAsync(Guid userId, CancellationToken token)
	{
		var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, token)
			?? throw new Exception(); //todo: exception
		return user.AuthenticationInfo ?? throw new Exception("This database is not configured for user authentication mode"); //todo: exception
	}
	public async Task<User?> TryFindUserAsync(Guid userId, CancellationToken token) => await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, token);
	public async Task<IAuthenticatedUser?> TryGetUserAsync(string normalizedUserName, CancellationToken cancellationToken)
	{
		var result = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToUpper() == normalizedUserName, cancellationToken);
		return result;
	}

	public async Task<IAuthenticatedUser?> TryGetUserAsync(Guid id, CancellationToken cancellationToken)
	{
		var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
		return result;
	}

	public Task UpdateAsync(IAuthenticatedUser user, CancellationToken cancellationToken)
	{
		_context.Users.Update((DatabaseUser)user);
		return Task.CompletedTask;
	}
}
