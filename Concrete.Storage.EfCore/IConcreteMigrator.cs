namespace Concrete.Storage.EfCore;

public interface IConcreteMigrator
{
	public Task<bool> EnsureCreatedAsync(CancellationToken token);
}
