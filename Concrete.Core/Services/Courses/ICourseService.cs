using Concrete.Core.Courses;

namespace Concrete.Core.Services.Courses;
public interface ICourseService
{
	Task<Guid> StartCourseAsync(Guid templateId, SubjectDate[] subjectDates, string courseCode, CancellationToken token);
	Task<CourseTemplate> CreateCourseTemplateAsync(string name, CancellationToken token);
}
