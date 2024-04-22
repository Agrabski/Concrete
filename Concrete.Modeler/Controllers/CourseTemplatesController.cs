﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Concrete.Core.Data;
using Concrete.Core.Template;

namespace Concrete.Modeler.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseTemplatesController(ConcreteContext context) : ControllerBase
{

	// GET: api/CourseTemplates
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CourseTemplate>>> GetCourseTemplates()
	{
		return await context.CourseTemplates.ToListAsync();
	}

	// GET: api/CourseTemplates/5
	[HttpGet("{id}")]
	public async Task<ActionResult<CourseTemplate>> GetCourseTemplate(Guid id)
	{
		var courseTemplate = await context.CourseTemplates.FindAsync(id);

		if (courseTemplate == null)
		{
			return NotFound();
		}

		return courseTemplate;
	}

	// PUT: api/CourseTemplates/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public async Task<IActionResult> PutCourseTemplate(Guid id, CourseTemplate courseTemplate)
	{
		if (id != courseTemplate.Id)
		{
			return BadRequest();
		}

		context.Entry(courseTemplate).State = EntityState.Modified;

		try
		{
			await context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!CourseTemplateExists(id))
				return NotFound();
			else
				throw;
		}

		return NoContent();
	}

	// POST: api/CourseTemplates
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<CourseTemplate>> CreateCourseTemplate()
	{
		var courseTemplate = new CourseTemplate()
		{
			Id = Guid.NewGuid(),
			ClassTemplates = [],
			Name = "New course"
		};
		context.CourseTemplates.Add(courseTemplate);
		await context.SaveChangesAsync();

		return CreatedAtAction("GetCourseTemplate", new { id = courseTemplate.Id }, courseTemplate);
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
