using Concrete.Core.Template;
using Concrete.Interface;
using Concrete.Interface.Templates;

namespace Concrete.Modeler.Client;

public interface IModelerClient
{
	Task<CourseTemplateHeader> CreateCourseTemplateAsync(CancellationToken token);
	Task<ActivityMetadata[]> GetAllActivitiesAsync(CancellationToken token);
	Task<CourseTemplateHeader[]> GetCoureTemplatesAsync(CancellationToken token);
	Task<CourseTemplateDetails> GetCourseTemplateAsync(Guid id, CancellationToken token);
}
