namespace Concrete.Core.Courses;

public class Course
{
	public required string CourseCode { get; init; }
	public required Guid Id { get; init; }
	public List<Subject> Subjects { get; init; } = new();
	public List<StudentGroup> StudentGroups { get; init; } = new();
}
