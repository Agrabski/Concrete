using Concrete.Core;
using Concrete.Core.Template;
using Concrete.Extensions.Quizes.Api;
using Concrete.Interface;

namespace Concrete.Extensions.Quizes;

public class QuizTemplate : IActivityTemplate<QuizTemplateData>
{
	public static readonly ActivityName ActivityName = new(MetadataConsts.ExtensionName, "Quiz");
	public ActivityName TypeName { get; } = ActivityName;
	public Guid Id { get; init; } = Guid.NewGuid();
	public LocalisedText DisplayName { get; set; } = new();
	public QuizTemplateData TemplateData { get; set; } = new();
}
