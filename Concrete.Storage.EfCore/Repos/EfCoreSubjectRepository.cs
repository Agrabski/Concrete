using Concrete.Core.Courses;
using Concrete.Core.Services.Subjects;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Storage.EfCore.Repos;

internal class EfCoreSubjectRepository : ISubjectRepository
{
	private readonly ConcreteContext _context;

	public EfCoreSubjectRepository(ConcreteContext context)
	{
		_context = context;
	}

	public Task AddSubjectAsync(Subject subject, CancellationToken token) => _context.Subjects.AddAsync(subject, token).AsTask();
	public Task<Subject?> TryGetSubjectAsync(Guid courseId, Guid subjectId, CancellationToken token) => _context.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId && s.CourseId == courseId, token);
}
