using Concrete.Core.Activities.Templates;
using Concrete.Localization;

namespace Concrete.Core.Courses;

public record Subject(Guid Id, IActivityTemplate[] Activities, LocalisedString Name, LocalisedString Description, Guid TemplateId, DateTime Date);
