using Concrete.Core.Courses;

namespace Concrete.Storage.EfCore;

internal record CourseTemplateProxy(Guid Id, string Name, CourseTemplate Template);
