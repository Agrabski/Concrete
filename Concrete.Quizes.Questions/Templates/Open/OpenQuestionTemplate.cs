using Concrete.Core.Activities.Templates;
using Concrete.Core.Questions.Instances;
using Concrete.Core.Questions.Templates;
using Concrete.Localization;
using Concrete.Quizes.Questions.Instances.Open;

namespace Concrete.Quizes.Questions.Templates.Open;

public class OpenQuestionTemplate : IQuestionTemplate<OpenQuestionAnswer>
{
	public required LocalisedStringTemplate Question { get; set; }
	public required Guid Id { get; init; }
	public List<string> ParameterNames { get; } = new();
	public required IEnumerable<QuestionTemplateInstance> AvailableInstances { get; set; }
	public bool AvailableInstancesAreFinite => false;
	public CategoryName Category { get; set; }

	public IQuestion<OpenQuestionAnswer> FillTemplate(Dictionary<string, object> parameters) => new OpenQuestion()
	{
		Parameters = parameters,
		Question = Question.Fill(parameters),
		QuestionId = Guid.NewGuid(),
		TemplateId = Id
	};
}
