using Concrete.Core.Courses;

namespace Concrete.Core.Services.Courses;
public interface ICourseRepository
{
	Task<Course?> TryGetCourseAsync(Guid courseId, CancellationToken token);
}
