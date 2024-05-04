using Concrete.Interface;
using Concrete.Modeler.Extension.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Modeler.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MenusController(IModelerExtensionClient client) : ControllerBase
{
	[HttpGet]
	public Task<MenuMetadata[]> GetMenuMetadataAsync(CancellationToken token)
	{
		return client.GetMenuMetadataAsync(token);
	}
}
