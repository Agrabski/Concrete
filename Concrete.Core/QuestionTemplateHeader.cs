using Concrete.Core.Activities.Templates;

namespace Concrete.Core;

public record struct QuestionTemplateHeader(Guid Id, string Name, CategoryName Category, string Type);
