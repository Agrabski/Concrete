using Concrete.Core.Courses;

namespace Concrete.Core.Services.Courses;
public interface ICourseRepository
{
	ValueTask UpdateAsync(CourseTemplate courseTemplate, CancellationToken token);
	ValueTask DeleteAsync(Guid courseTemplateId, CancellationToken token);
	Task AddAsync(CourseTemplate template, CancellationToken token);
	Task AddAsync(Course instance, CancellationToken token);
	Task<Course?> TryGetCourseAsync(Guid courseId, CancellationToken token);
	Task<CourseTemplate?> TryGetCourseTemplateAsync(Guid templateId, CancellationToken token);
	IAsyncEnumerable<CourseTemplateHeader> ListAsync(CancellationToken token);
}
