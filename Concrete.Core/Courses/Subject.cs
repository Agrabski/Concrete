using Concrete.Localization;

namespace Concrete.Core.Courses;

public class Subject
{
	public required Guid CourseId { get; init; }
	public required Guid Id { get; init; }
	public required SubjectActivity[] Activities { get; set; }
	public required LocalisedString Name { get; init; }
	public required LocalisedString Description { get; init; }
	public Guid TemplateId { get; init; }
	public required SubjectDateForGroup[] DatesForGroups { get; set; }
}
