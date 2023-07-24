using Concrete.Core.Courses;
using Concrete.Core.Services.Courses;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Storage.EfCore.Repos;
internal class EfCoreCourseRepository : ICourseRepository
{
	private readonly ConcreteContext _context;

	public EfCoreCourseRepository(ConcreteContext context)
	{
		_context = context;
	}

	public Task AddAsync(CourseTemplate template, CancellationToken token) => _context.CourseTemplates.AddAsync(new(template.TemplateId, template), token).AsTask();
	public Task AddAsync(Course instance, CancellationToken token) => _context.Courses.AddAsync(instance, token).AsTask();
	public async ValueTask DeleteAsync(Guid courseTemplateId, CancellationToken token) => _context.CourseTemplates.Remove(await _context.CourseTemplates.FirstAsync(c => c.Id == courseTemplateId, token));
	public Task<Course?> TryGetCourseAsync(Guid courseId, CancellationToken token) => _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId, token);
	public async Task<CourseTemplate?> TryGetCourseTemplateAsync(Guid templateId, CancellationToken token) => (await _context.CourseTemplates.FirstOrDefaultAsync(c => c.Id == templateId, token))?.Template;
	public async ValueTask UpdateAsync(CourseTemplate courseTemplate, CancellationToken token)
	{
		if (await _context.CourseTemplates.AnyAsync(c => c.Id == courseTemplate.TemplateId, token))
			_context.Update(new CourseTemplateProxy(courseTemplate.TemplateId, courseTemplate));
		else
			_context.Add(new CourseTemplateProxy(courseTemplate.TemplateId, courseTemplate));
	}
}
