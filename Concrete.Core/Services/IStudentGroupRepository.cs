namespace Concrete.Core.Services;

public interface IStudentGroupRepository
{
	Task<StudentGroup?> TryGetStudentGroupAsync(Guid id, CancellationToken token);
}
