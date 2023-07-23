using Concrete.Localization;

namespace Concrete.Core.Courses;

public record SubjectTemplate(SubjectActivity[] Activities, LocalisedString Name, LocalisedString Description, Guid Id)
{
	public Subject FillTemplate(DateTime startTime, Guid courseId)
	{
		return new()
		{
			Activities = Activities,
			CourseId = courseId,
			Date = startTime,
			Description = Description,
			Name = Name,
			Id = Guid.NewGuid(),
			TemplateId = Id
		};
	}
}
