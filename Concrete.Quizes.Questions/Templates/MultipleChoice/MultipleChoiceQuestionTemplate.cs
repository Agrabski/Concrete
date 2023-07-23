using Concrete.Core.Questions.Instances;
using Concrete.Core.Questions.Templates;
using Concrete.Localization;
using Concrete.Quizes.Questions.Instances.MultipleChoice;
using Concrete.Quizes.Questions.Instances.MultipleChoice.Grading;

namespace Concrete.Quizes.Questions.Templates.MultipleChoice;
public class MultipleChoiceQuestionTemplate : IQuestionTemplate<MultipleChoiceQuestionAnswerSet> 
{
	public required LocalisedStringTemplate Question { get; set; }
	public List<MultipleChoiceQuestionAnswerTemplate> Answers { get; init; } = new();
	public Guid Id { get; }
	public List<string> ParameterNames { get; init; } = new();
	// todo
	public IEnumerable<QuestionTemplateInstance> AvailableInstances { get; } = Array.Empty<QuestionTemplateInstance>();
	public required IGrading Grading { get; init; }

	public IQuestion<MultipleChoiceQuestionAnswerSet> FillTemplate(Dictionary<string, object> parameters)
	{
		return new MultipleChoiceQuestion()
		{
			Parameters = parameters,
			TemplateId = Id,
			QuestionId = Guid.NewGuid(),
			Answers = Answers
				.Select(a => new MultipleChoiceQuestionAnswer(a.Index, a.Text.Fill(parameters)))
				.ToList(),
			Question = Question.Fill(parameters),
			Grading = Grading
		};
	}
}
