namespace Concrete.Core.Services.QuestionBanks;
public interface IQuestionBankRepository
{
	public Task<IQuestionBank?> TryGet(Guid questionBankId, CancellationToken token);
	public Task AddAsync(IQuestionBank bank, CancellationToken token);
	Task UpdateAsync(QuestionBank bank, CancellationToken token);
}
