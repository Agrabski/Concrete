namespace Concrete.Core.Courses;
public class CourseTemplate
{
	public Guid TemplateId { get; init; }
	public List<SubjectTemplate> Subjects { get; init; } = new();
	public Course FillTemplate(SubjectDate[] subjectDates)
	{
		var courseId = Guid.NewGuid();
		return new Course()
		{
			Id = courseId,
			Subjects = Subjects
			.Select(s => s.FillTemplate(subjectDates.First(d => d.SubjectId == s.Id).Time, courseId))
			.ToList()
		};
	}
}
