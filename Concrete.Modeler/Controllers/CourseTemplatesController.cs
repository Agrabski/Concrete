﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Concrete.Core.Data;
using Concrete.Core.Template;
using Concrete.Interface.Templates;

namespace Concrete.Modeler.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseTemplatesController(ConcreteContext context) : ControllerBase
{

	// GET: api/CourseTemplates
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CourseTemplateHeader>>> GetCourseTemplates()
	{
		return await context.CourseTemplates.Select(t => new CourseTemplateHeader(t.Id, t.Name, t.ClassTemplates.Count)).ToListAsync();
	}

	// GET: api/CourseTemplates/5
	[HttpGet("{id}")]
	public async Task<ActionResult<CourseTemplateDetails>> GetCourseTemplate(Guid id, CancellationToken token)
	{
		var courseTemplate = await context.CourseTemplates.FirstOrDefaultAsync(t => t.Id == id, token);

		if (courseTemplate == null)
			return NotFound();
		var classes = await context
			.CourseTemplates
			.Where(c => c.Id == id)
			.SelectMany(t => t.ClassTemplates)
			.Select(t => new ClassTemplateHeader(t.Id, t.Name, t.ActivityTemplates.Count))
			.ToArrayAsync(token);
		return Ok(new CourseTemplateDetails(
			courseTemplate.Id,
			courseTemplate.Name,
			classes
		));
	}

	[HttpPut("{courseId}")]
	public async Task<ActionResult<ClassTemplateHeader>> CreateClassTemplate(
		Guid courseId,
		[FromBody] string name,
		CancellationToken token
	)
	{
		if (await context.CourseTemplates.FirstOrDefaultAsync(t => t.Id == courseId, token) is not CourseTemplate template)
			return NotFound($"Course template {courseId} does not exists");
		ClassTemplate result = new()
		{
			Name = name,
			ActivityTemplates = [],
			Id = Guid.NewGuid(),
		};
		template.ClassTemplates.Add(result);
		await context.SaveChangesAsync(token);
		return Ok(new ClassTemplateHeader(result.Id, result.Name, 0));
	}

	[HttpPost]
	public async Task<ActionResult<CourseTemplateHeader>> CreateCourseTemplate()
	{
		var courseTemplate = new CourseTemplate()
		{
			Id = Guid.NewGuid(),
			ClassTemplates = [],
			Name = "New course"
		};
		context.CourseTemplates.Add(courseTemplate);
		await context.SaveChangesAsync();

		return CreatedAtAction("GetCourseTemplate", new { id = courseTemplate.Id }, new CourseTemplateHeader(courseTemplate.Id, courseTemplate.Name, 0));
	}

	// DELETE: api/CourseTemplates/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteCourseTemplate(Guid id)
	{
		var courseTemplate = await context.CourseTemplates.FindAsync(id);
		if (courseTemplate == null)
		{
			return NotFound();
		}

		context.CourseTemplates.Remove(courseTemplate);
		await context.SaveChangesAsync();

		return NoContent();
	}

	private bool CourseTemplateExists(Guid id)
	{
		return context.CourseTemplates.Any(e => e.Id == id);
	}
}
