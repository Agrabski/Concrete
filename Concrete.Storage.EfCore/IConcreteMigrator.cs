using Concrete.Users;

namespace Concrete.Storage.EfCore;

public interface IConcreteMigrator
{
	public Task EnsureCreatedAsync(CancellationToken token);
}

internal class DatabaseUser : User { }
