using Concrete.Core.Template;
using Concrete.Interface;

namespace Concrete.Modeler.Client;

public interface IModelerClient
{
	Task<CourseTemplate> CreateCourseTemplateAsync(CancellationToken token);
	Task<ActivityMetadata[]> GetAllActivitiesAsync(CancellationToken token);
}
