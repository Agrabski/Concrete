namespace Concrete.Storage.EfCore;

internal class ConcreteMigrator : IConcreteMigrator
{
	private readonly ConcreteContext _concreteContext;

	public ConcreteMigrator(ConcreteContext concreteContext)
	{
		_concreteContext = concreteContext;
	}

	public async Task<bool> EnsureCreatedAsync(CancellationToken token)
	{
		var result = await _concreteContext.Database.EnsureCreatedAsync(token);
		await _concreteContext.SaveChangesAsync(token);
		await _concreteContext.SaveChangesAsync(token);
		return result;
	}
}
