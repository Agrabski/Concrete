namespace Concrete.Storage.EfCore;

internal class ConcreteMigrator : IConcreteMigrator
{
	private readonly ConcreteContext _concreteContext;

	public ConcreteMigrator(ConcreteContext concreteContext)
	{
		_concreteContext = concreteContext;
	}

	public Task EnsureCreatedAsync(CancellationToken token) => _concreteContext.Database.EnsureCreatedAsync(token);
}
