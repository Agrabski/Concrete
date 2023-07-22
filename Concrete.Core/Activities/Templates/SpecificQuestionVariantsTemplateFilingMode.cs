namespace Concrete.Core.Activities.Templates;

public record SpecificQuestionVariantsTemplateFilingMode(Guid[] VariantIds) : IQuestionTemplateFilingMode;
