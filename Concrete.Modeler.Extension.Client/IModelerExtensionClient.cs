using Concrete.Core.Template;
using Concrete.Interface;

namespace Concrete.Modeler.Extension.Client;

public interface IModelerExtensionClient
{
	Task<ActivityTemplate> CreateTemplateAsync(ActivityName name, CancellationToken token);
	Task<ActivityMetadata[]> GetAllActivitiesAsync(CancellationToken token);
}

