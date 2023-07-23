namespace Concrete.Core.Courses;

public class Course
{
	public Guid Id { get; init; }
	public List<Subject> Subjects { get; init; } = new();
}
