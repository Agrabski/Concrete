namespace Concrete.Core.Services;
public interface IConcreteUnitOfWork
{
	public Task CommitAsync(CancellationToken token);
}
