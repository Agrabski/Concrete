using Concrete.Core.Activities.Templates;
using Concrete.Localization;

namespace Concrete.Core.Course;

public record SubjectTemplate(IActivityTemplate[] Activities, LocalisedString Name, LocalisedString Description, Guid Id);
