using Concrete.Core;

namespace Concrete.Storage.EfCore.Repos;

internal class QuestionBankProxy
{
	public Guid Id { get; init; }
	public required IQuestionBank QuestionBank { get; set; }
}
