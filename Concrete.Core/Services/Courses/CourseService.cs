﻿using Concrete.Core.Courses;

namespace Concrete.Core.Services.Courses;
internal class CourseService
{
	private readonly ICourseRepository _courseRepository;

	public CourseService(ICourseRepository courseRepository)
	{
		_courseRepository = courseRepository;
	}

	public async Task<Guid> StartCourse(Guid templateId, SubjectDate[] subjectDates, CancellationToken token)
	{
		var template = await _courseRepository.TryGetCourseTemplateAsync(templateId, token)
			?? throw new Exception("Course template not found"); //todo
		var instance = template.FillTemplate(subjectDates);
		await _courseRepository.AddAsync(instance, token);
		return instance.Id;
	}
}
