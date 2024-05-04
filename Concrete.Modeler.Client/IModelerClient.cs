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
	Task<ClassTemplateDetails> GetClassTemplateAsync(Guid id, CancellationToken token);

	Task UpdateCourseTemplateNameAsync(Guid id, string name, CancellationToken token);
	Task<ClassTemplateHeader> CreateCourseClassTemplateAsync(Guid courseTemplateId, string name, CancellationToken token);
	Task<Uri> GetExtensionEditorForActivityTypeAsync(ActivityTypeName name, CancellationToken token);
	Task<ActivityTemplate> CreateActivityTemplate(Guid classId, ActivityTypeName name, CancellationToken token);
	Task<MenuMetadata[]> GetMenuMetadataAsync(CancellationToken token);
}
