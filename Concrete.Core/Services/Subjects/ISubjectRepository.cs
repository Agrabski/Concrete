using Concrete.Core.Courses;

namespace Concrete.Core.Services.Subjects;
public interface ISubjectRepository
{
	public Task<Subject?> TryGetSubjectAsync(Guid courseId, Guid subjectId, CancellationToken token);
	public Task AddSubjectAsync(Subject subject, CancellationToken token);
}
