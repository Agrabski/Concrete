namespace Concrete.Core.Services;
public interface IStudentGroupService
{
	Task AddStudentsToGroup(Guid groupId, IEnumerable<Guid> studentIds, CancellationToken cancellationToken);
}
