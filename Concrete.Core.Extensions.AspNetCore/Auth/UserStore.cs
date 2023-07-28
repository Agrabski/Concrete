using Concrete.Users;
using Microsoft.AspNetCore.Identity;

namespace Concrete.Core.Extensions.AspNetCore.Auth;
internal class UserStore : IUserPasswordStore<IAuthenticatedUser>
{
	private readonly IAuthenticationInfoRepository _authenticationInfoRepository;

	public UserStore(IAuthenticationInfoRepository authenticationInfoRepository)
	{
		_authenticationInfoRepository = authenticationInfoRepository;
	}

	public async Task<IdentityResult> CreateAsync(IAuthenticatedUser user, CancellationToken cancellationToken)
	{
		await _authenticationInfoRepository.AddAuthenticatedUserAsync(user, cancellationToken);
		return new();
	}
	public Task<IdentityResult> DeleteAsync(IAuthenticatedUser user, CancellationToken cancellationToken) => throw new NotImplementedException();
	public void Dispose() { }
	public Task<IAuthenticatedUser?> FindByIdAsync(string userId, CancellationToken cancellationToken) => _authenticationInfoRepository.TryGetUserAsync(Guid.Parse(userId), cancellationToken);
	public Task<IAuthenticatedUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) => _authenticationInfoRepository.TryGetUserAsync(normalizedUserName, cancellationToken);
	public Task<string?> GetNormalizedUserNameAsync(IAuthenticatedUser user, CancellationToken cancellationToken) => Task.FromResult<string?>(user.UserName.ToUpperInvariant());
	public Task<string?> GetPasswordHashAsync(IAuthenticatedUser user, CancellationToken cancellationToken) => Task.FromResult(user.GetAuthentication().PasswordHash);
	public Task<string> GetUserIdAsync(IAuthenticatedUser user, CancellationToken cancellationToken) => Task.FromResult(user.Id.ToString());
	public Task<string?> GetUserNameAsync(IAuthenticatedUser user, CancellationToken cancellationToken) => Task.FromResult<string?>(user.UserName);
	public Task<bool> HasPasswordAsync(IAuthenticatedUser user, CancellationToken cancellationToken) => Task.FromResult(true);
	public Task SetNormalizedUserNameAsync(IAuthenticatedUser user, string? normalizedName, CancellationToken cancellationToken) => throw new NotImplementedException();
	public Task SetPasswordHashAsync(IAuthenticatedUser user, string? passwordHash, CancellationToken cancellationToken)
	{
		user.GetAuthentication().PasswordHash = passwordHash;
		return Task.CompletedTask;
	}
	public Task SetUserNameAsync(IAuthenticatedUser user, string? userName, CancellationToken cancellationToken) => throw new NotImplementedException();
	public Task<IdentityResult> UpdateAsync(IAuthenticatedUser user, CancellationToken cancellationToken) => throw new NotImplementedException();
}
