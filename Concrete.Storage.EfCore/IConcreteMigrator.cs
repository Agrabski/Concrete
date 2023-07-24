namespace Concrete.Storage.EfCore;

public interface IConcreteMigrator
{
	public Task EnsureCreatedAsync(CancellationToken token);
}
