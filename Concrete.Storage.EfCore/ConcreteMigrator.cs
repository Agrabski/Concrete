namespace Concrete.Storage.EfCore;

internal class ConcreteMigrator : IConcreteMigrator
{
	private readonly ConcreteContext _concreteContext;

	public ConcreteMigrator(ConcreteContext concreteContext)
	{
		_concreteContext = concreteContext;
	}

	public async Task EnsureCreatedAsync(CancellationToken token)
	{
		await _concreteContext.Database.EnsureCreatedAsync(token);
		await _concreteContext.SaveChangesAsync(token);
	}
}
