namespace Concrete.Core.Courses;
public class CourseTemplate
{
	public Guid TemplateId { get; init; }
	public List<SubjectTemplate> Subjects { get; init; } = new();
	public Course FillTemplate(SubjectDate[] subjectDates, string courseCode)
	{
		var courseId = Guid.NewGuid();
		return new Course()
		{
			CourseCode = courseCode,
			Id = courseId,
			Subjects = Subjects
			.Select(s => s.FillTemplate(subjectDates.First(d => d.SubjectId == s.Id).DateForGroups, courseId))
			.ToList()
		};
	}
}
