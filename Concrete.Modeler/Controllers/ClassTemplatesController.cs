using Concrete.Core.Data;
using Concrete.Interface.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Modeler.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClassTemplatesController(ConcreteContext dbContext) : ControllerBase
{
	[HttpGet("{id}")]
	public async Task<ActionResult<ClassTemplateDetails>> GetAsync(Guid id, CancellationToken token)
	{
		var result = await dbContext.ClassTemplates.Include(c=>c.ActivityTemplates).FirstOrDefaultAsync(x=>x.Id == id, token);
		if(result is null)
			return NotFound();
		return Ok(new ClassTemplateDetails(
			result.Id,
			result.Name,
			result.ActivityTemplates.Select(a=>new ActivityTemplateHeader(a.Id, a.TypeName, a.Name)).ToArray()
		));
	}
}
