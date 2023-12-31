﻿using Microsoft.AspNetCore.Identity;

namespace Concrete.Core.Extensions.AspNetCore.Auth;
internal class RoleStore : IRoleStore<UserRole>
{
	public Task<IdentityResult> CreateAsync(UserRole role, CancellationToken cancellationToken) => throw new NotImplementedException();
	public Task<IdentityResult> DeleteAsync(UserRole role, CancellationToken cancellationToken) => throw new NotImplementedException();
	public void Dispose() { }
	public Task<UserRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken) => throw new NotImplementedException();
	public Task<UserRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken) => throw new NotImplementedException();
	public Task<string?> GetNormalizedRoleNameAsync(UserRole role, CancellationToken cancellationToken) => throw new NotImplementedException();
	public Task<string> GetRoleIdAsync(UserRole role, CancellationToken cancellationToken) => throw new NotImplementedException();
	public Task<string?> GetRoleNameAsync(UserRole role, CancellationToken cancellationToken) => throw new NotImplementedException();
	public Task SetNormalizedRoleNameAsync(UserRole role, string? normalizedName, CancellationToken cancellationToken) => throw new NotImplementedException();
	public Task SetRoleNameAsync(UserRole role, string? roleName, CancellationToken cancellationToken) => throw new NotImplementedException();
	public Task<IdentityResult> UpdateAsync(UserRole role, CancellationToken cancellationToken) => throw new NotImplementedException();
}
