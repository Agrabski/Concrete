﻿using Concrete.Core.Activities.Templates;
using Concrete.Core.Questions.Templates;

namespace Concrete.Core;
public class QuestionBank : IQuestionBank
{
	public List<IQuestionTemplate> QuestionTemplates { get; init; } = new();
	public Guid Id { get; init; } = Guid.NewGuid();
	public void AddQuestion(IQuestionTemplate questionTemplate)
	{
		QuestionTemplates.Add(questionTemplate);
	}

	public IEnumerable<IQuestionTemplate> GetQuestionsByCategory(CategoryName categoryName) => QuestionTemplates.Where(q => categoryName.Contains(q.Category));

	public IQuestionTemplate? TryGetQuestionTemplate(Guid id)
	{
		return QuestionTemplates.FirstOrDefault(q => q.Id == id);
	}
}
