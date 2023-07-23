using Concrete.Localization;

namespace Concrete.Core.Courses;

public record SubjectTemplate(SubjectActivity[] Activities, LocalisedString Name, LocalisedString Description, Guid Id)
{
	public Subject FillTemplate(SubjectDateForGroup[] startTime, Guid courseId)
	{
		return new()
		{
			Activities = Activities,
			CourseId = courseId,
			DatesForGroups = startTime,
			Description = Description,
			Name = Name,
			Id = Guid.NewGuid(),
			TemplateId = Id
		};
	}
}
