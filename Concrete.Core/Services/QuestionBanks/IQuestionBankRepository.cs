namespace Concrete.Core.Services.QuestionBanks;
public interface IQuestionBankRepository
{
	public Task<QuestionBank?> TryGet(Guid questionBankId, CancellationToken token);
	public Task AddAsync(QuestionBank bank, CancellationToken token);
	Task UpdateAsync(QuestionBank bank, CancellationToken token);
	Task<List<QuestionBankHeader>> GetAllQuestionBanksAsync(CancellationToken token);
}
