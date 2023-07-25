using Concrete.Users;

namespace Concrete.Core.Services;

internal class StudentGroupService : IStudentGroupService
{
	private readonly IUserRepository _userRepository;
	private readonly IStudentGroupRepository _studentGroupRepository;

	public StudentGroupService(IUserRepository userRepository, IStudentGroupRepository studentGroupRepository)
	{
		_userRepository = userRepository;
		_studentGroupRepository = studentGroupRepository;
	}

	public async Task AddStudentsToGroup(Guid groupId, IEnumerable<Guid> studentIds, CancellationToken cancellationToken)
	{
		var group = (await _studentGroupRepository.TryGetStudentGroupAsync(groupId, cancellationToken))
			?? throw new Exception("Group not found");
		foreach (var student in studentIds
			.Select(async id => (await _userRepository.TryFindUserAsync(id, cancellationToken))
			?? throw new Exception($"student {id} not found"))
		)
			group.Users.Add(await student);
	}
}
