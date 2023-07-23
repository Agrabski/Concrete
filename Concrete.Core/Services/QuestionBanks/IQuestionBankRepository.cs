namespace Concrete.Core.Services.QuestionBanks;
public interface IQuestionBankRepository
{
	public Task<IQuestionBank?> TryGet(Guid questionBankId, CancellationToken token);
}
