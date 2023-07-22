using Concrete.Core.Activities.Templates;
using Concrete.Localization;

namespace Concrete.Core.Course;

public record Subject(IActivityTemplate[] Activities, LocalisedString Name, LocalisedString Description, Guid TemplateId);
