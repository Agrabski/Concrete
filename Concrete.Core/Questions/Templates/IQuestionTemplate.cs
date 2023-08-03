using Concrete.Core.Activities.Templates;
using Concrete.Core.Questions.Instances;

namespace Concrete.Core.Questions.Templates;

public interface IQuestionTemplate
{
	Guid Id { get; }
	public List<string> ParameterNames { get; }
	IQuestion FillQuestionTemplate(Dictionary<string, object> parameters);
	public IEnumerable<QuestionTemplateInstance> AvailableInstances { get; }
	public bool AvailableInstancesAreFinite { get; }
	CategoryName Category { get; set; }
}
public interface IQuestionTemplate<TAnswer> : IQuestionTemplate where TAnswer : IQuestionAnswer
{
	IQuestion IQuestionTemplate.FillQuestionTemplate(Dictionary<string, object> parameters) => FillTemplate(parameters);
	IQuestion<TAnswer> FillTemplate(Dictionary<string, object> parameters);
}
