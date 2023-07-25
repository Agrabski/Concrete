using Concrete.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StudentGroupController : ControllerBase
{
	private readonly IStudentGroupService _studentGroupService;
	private readonly IConcreteUnitOfWork _unitOfWork;

	public StudentGroupController(IStudentGroupService studentGroupService, IConcreteUnitOfWork unitOfWork)
	{
		_studentGroupService = studentGroupService;
		_unitOfWork = unitOfWork;
	}

	[HttpPost("/add/{groupId}")]
	public async Task AddUsersToGroup([FromRoute] Guid groupId, [FromBody] Guid[] userIds, CancellationToken token)
	{
		await _studentGroupService.AddStudentsToGroup(groupId, userIds, token);
		await _unitOfWork.CommitAsync(token);
	}
}
