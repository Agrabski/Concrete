using Concrete.Core.Activities.Instances;

namespace Concrete.Core.Services.Activities;
public interface IActivityInstanceRepository
{
	public Task AddAsync(IActivity activity, CancellationToken token);
	public Task<T?> TryFindAsync<T>(Guid activityInstance, CancellationToken token) where T : class, IActivity;
}
