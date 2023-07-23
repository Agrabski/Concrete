using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Concrete.Server.Controllers;
[Route("api/Course/Template")]
[ApiController]
public class CourseTemplateController : ControllerBase
{
	// GET: api/<CourseTemplateController>
	[HttpGet]
	public IEnumerable<string> Get()
	{
		return new string[] { "value1", "value2" };
	}

	// GET api/<CourseTemplateController>/5
	[HttpGet("{id}")]
	public string Get(int id)
	{
		return "value";
	}

	// POST api/<CourseTemplateController>
	[HttpPost]
	public void Post([FromBody] string value)
	{
	}

	// PUT api/<CourseTemplateController>/5
	[HttpPut("{id}")]
	public void Put(int id, [FromBody] string value)
	{
	}

	// DELETE api/<CourseTemplateController>/5
	[HttpDelete("{id}")]
	public void Delete(int id)
	{
	}
}
