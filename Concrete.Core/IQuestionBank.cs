using Concrete.Core.Activities.Templates;
using Concrete.Core.Questions.Templates;
using System.Text.Json.Serialization;

namespace Concrete.Core;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(QuestionBank), "bank")]
public interface IQuestionBank
{
	Guid Id { get; init; }

	void AddQuestion(IQuestionTemplate questionTemplate);
	IQuestionTemplate? TryGetQuestionTemplate(Guid id);
	IEnumerable<IQuestionTemplate> GetQuestionsByCategory(CategoryName categoryName);
	IEnumerable<CategoryName> Categories { get; }
}
