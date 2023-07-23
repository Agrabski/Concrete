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

	public Task AddAsync(CourseTemplate template, CancellationToken token) => _context.CourseTemplates.AddAsync(template, token).AsTask();
	public Task AddAsync(Course instance, CancellationToken token) => _context.Courses.AddAsync(instance, token).AsTask();
	public Task<Course?> TryGetCourseAsync(Guid courseId, CancellationToken token) => _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId, token);
	public Task<CourseTemplate?> TryGetCourseTemplateAsync(Guid templateId, CancellationToken token) => _context.CourseTemplates.FirstOrDefaultAsync(c => c.TemplateId == templateId, token);
}
