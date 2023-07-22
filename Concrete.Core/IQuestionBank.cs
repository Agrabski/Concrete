using Concrete.Core.Questions.Templates;
using System.Text.Json.Serialization;

namespace Concrete.Core;

[JsonPolymorphic]
[JsonDerivedType(typeof(QuestionBank))]
public interface IQuestionBank
{
	void AddQuestion(IQuestionTemplate questionTemplate);
	IQuestionTemplate? TryGetQuestionTemplate(Guid id);
}
