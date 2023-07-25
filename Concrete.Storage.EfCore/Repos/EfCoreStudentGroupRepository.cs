using Concrete.Core;
using Concrete.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Storage.EfCore.Repos;

internal class EfCoreStudentGroupRepository : IStudentGroupRepository
{
	private readonly ConcreteContext _context;

	public EfCoreStudentGroupRepository(ConcreteContext context)
	{
		_context = context;
	}

	public Task<StudentGroup?> TryGetStudentGroupAsync(Guid id, CancellationToken token) => _context
		.StudentGroups
		.Include(s => s.Users)
		.FirstOrDefaultAsync(g => g.Id == id, token);
}
