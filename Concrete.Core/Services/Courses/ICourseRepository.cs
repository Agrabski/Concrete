using Concrete.Core.Courses;

namespace Concrete.Core.Services.Courses;
public interface ICourseRepository
{
	Task AddAsync(CourseTemplate template, CancellationToken token);
	Task AddAsync(Course instance, CancellationToken token);
	Task<Course?> TryGetCourseAsync(Guid courseId, CancellationToken token);
	Task<CourseTemplate?> TryGetCourseTemplateAsync(Guid templateId, CancellationToken token);
}
