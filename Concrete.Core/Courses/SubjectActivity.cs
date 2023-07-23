using Concrete.Core.Activities.Templates;

namespace Concrete.Core.Courses;

public class SubjectActivity
{
	public required Guid Id { get; init; }
	public required IActivityTemplate Template { get; init; }

}
