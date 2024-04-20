using Concrete.Interface;

namespace Concrete.Modeler.Extension.Client;

public interface IModelerExtensionClient
{
	Task<ActivityMetadata[]> GetAllActivitiesAsync(CancellationToken token);
}

