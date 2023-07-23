using Concrete.Core.Activities.Templates;
using Concrete.Localization;

namespace Concrete.Core.Courses;

public record SubjectTemplate(IActivityTemplate[] Activities, LocalisedString Name, LocalisedString Description, Guid Id);
