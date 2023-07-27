using Concrete.Core.Activities.Instances;
using Concrete.Core.Services.Activities;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Storage.EfCore.Repos;

internal class EfCoreActivityInstanceRepository : IActivityInstanceRepository
{
	private readonly ConcreteContext _context;

	public EfCoreActivityInstanceRepository(ConcreteContext context)
	{
		_context = context;
	}

	public Task AddAsync(IActivity activity, CancellationToken token) => _context.ActivityInstances.AddAsync(new(activity.InstanceId, activity), token).AsTask();
	public async Task<T?> TryFindAsync<T>(Guid activityInstance, CancellationToken token) where T : class, IActivity => (await _context.ActivityInstances.FirstOrDefaultAsync(a => a.Id == activityInstance, token))?.Instance as T;
}
