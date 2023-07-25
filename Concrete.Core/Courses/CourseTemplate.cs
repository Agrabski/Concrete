namespace Concrete.Core.Courses;
public class CourseTemplate
{
	public required Guid TemplateId { get; init; }
	public required string TemplateName { get; set; }
	public List<SubjectTemplate> Subjects { get; init; } = new();
	public Course FillTemplate(SubjectDate[] subjectDates, string courseCode)
	{
		var courseId = Guid.NewGuid();
		return new Course()
		{
			Name = TemplateName,
			CourseCode = courseCode,
			Id = courseId,
			Subjects = Subjects
			.Select(s => s.FillTemplate(subjectDates.First(d => d.SubjectId == s.Id).DateForGroups, courseId))
			.ToList(),
			StudentGroups = subjectDates
				.SelectMany(d => d.DateForGroups.Select(d => d.GroupId))
				.Distinct()
				.Select((id, index) => new StudentGroup()
				{
					Id = id,
					Name = $"group {index}"
				}).ToList(),
		};
	}
}
