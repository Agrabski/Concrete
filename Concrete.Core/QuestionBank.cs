using Concrete.Core.Questions.Templates;

namespace Concrete.Core;
public class QuestionBank
{
	private readonly Dictionary<Guid, IQuestionTemplate> _questionTemplates = new();
	public Guid Id { get; init; } = Guid.NewGuid();
	public void AddQuestion(IQuestionTemplate questionTemplate)
	{
		_questionTemplates.Add(questionTemplate.Id, questionTemplate);
	}

	public IQuestionTemplate? TryGetQuestionTemplate(Guid id)
	{
		if (_questionTemplates.TryGetValue(id, out var question))
			return question;
		return null;
	}
}
