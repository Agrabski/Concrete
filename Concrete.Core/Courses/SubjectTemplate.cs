using Concrete.Localization;

namespace Concrete.Core.Courses;

public record SubjectTemplate(SubjectActivity[] Activities, LocalisedString Name, LocalisedString Description, Guid Id, string TempalteName)
{
	public Subject FillTemplate(SubjectDateForGroup[] startTime, Guid courseId)
	{
		return new()
		{
			Activities = Activities,
			CourseId = courseId,
			DatesForGroups = startTime.ToList(),
			Description = Description,
			Name = Name,
			Id = Guid.NewGuid(),
			TemplateId = Id
		};
	}
}
