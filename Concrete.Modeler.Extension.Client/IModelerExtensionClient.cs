using Concrete.Core.Template;
using Concrete.Interface;

namespace Concrete.Modeler.Extension.Client;

public interface IModelerExtensionClient
{
	Task<ActivityTemplate> CreateTemplateAsync(ActivityTypeName name, CancellationToken token);
	Task<ActivityMetadata[]> GetAllActivitiesAsync(CancellationToken token);
	ValueTask<Uri> GetExtensionActivityEditorAsync(ActivityTypeName name, CancellationToken token);
}

