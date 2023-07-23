using Concrete.Core.Courses;

namespace Concrete.Core.Services.Courses;
public interface ICourseService
{
	Task<Guid> StartCourse(Guid templateId, SubjectDate[] subjectDates, string courseCode, CancellationToken token);
}
