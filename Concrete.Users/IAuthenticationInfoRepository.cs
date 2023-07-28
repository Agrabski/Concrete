namespace Concrete.Users;

public interface IAuthenticationInfoRepository
{
	Task<AuthenticationInfo> GetAuthenticationInfoForUserAsync(Guid userId, CancellationToken token);
	Task AddAuthenticatedUserAsync(IAuthenticatedUser user, CancellationToken token);
	Task<IAuthenticatedUser?> TryGetUserAsync(string normalizedUserName, CancellationToken cancellationToken);
	Task<IAuthenticatedUser?> TryGetUserAsync(Guid id, CancellationToken cancellationToken);
}
