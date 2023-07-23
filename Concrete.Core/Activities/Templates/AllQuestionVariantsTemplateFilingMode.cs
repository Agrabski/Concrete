using Concrete.Core.Questions.Templates;

namespace Concrete.Core.Activities.Templates;

public record AllQuestionVariantsTemplateFilingMode : IQuestionTemplateFilingMode
{
	public QuestionTemplateInstance Fill(IQuestionTemplate template)
	{
		var instances = template.AvailableInstances;
		if (template.AvailableInstancesAreFinite)
		{
			var arr = instances.ToArray();
			return arr[new Random().Next(0, arr.Length)];
		}
		return instances.First();

	}
}
