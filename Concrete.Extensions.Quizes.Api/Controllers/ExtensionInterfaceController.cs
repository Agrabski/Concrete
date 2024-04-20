using Concrete.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Extensions.Quizes.Api.Controllers;
[Route("api/activities")]
[ApiController]
public class ExtensionInterfaceController : ControllerBase
{
	[HttpGet]
	public Task<ActivityMetadata[]> GetActivityMetadata(CancellationToken cancellationToken)
	{
		return Task.FromResult(new ActivityMetadata[]
		{
			new(new(new("Concrete","Core","Quiz"), "Test"))
		});
	}
}
