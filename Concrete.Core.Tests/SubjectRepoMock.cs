using Concrete.Core.Courses;
using Concrete.Core.Services.Subjects;

namespace Concrete.Core.Tests;

internal class SubjectRepoMock : ISubjectRepository
{
	private readonly Dictionary<Guid, Subject> _subjects;

	public SubjectRepoMock(Dictionary<Guid, Subject> subjects)
	{
		_subjects = subjects;
	}

	public Task<Subject?> TryGetSubjectAsync(Guid courseId, Guid subjectId, CancellationToken token)
	{
		if(_subjects.TryGetValue(subjectId, out var subject)) 
			return Task.FromResult<Subject?>(subject);
		return Task.FromResult<Subject?>(null);
	}
}
