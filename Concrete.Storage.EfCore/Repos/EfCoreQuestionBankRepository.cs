using Concrete.Core;
using Concrete.Core.Services.QuestionBanks;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Storage.EfCore.Repos;

internal class EfCoreQuestionBankRepository : IQuestionBankRepository
{
	private readonly ConcreteContext _context;

	public EfCoreQuestionBankRepository(ConcreteContext context)
	{
		_context = context;
	}

	public Task AddAsync(QuestionBank bank, CancellationToken token) => _context.QuestionBanks.AddAsync(new QuestionBankProxy { Id = bank.Id, QuestionBank = bank }, token).AsTask();
	public Task<List<QuestionBankHeader>> GetAllQuestionBanksAsync(CancellationToken token) => _context.QuestionBanks.Select(b => new QuestionBankHeader(b.QuestionBank.Name, b.Id)).ToListAsync(token);

	public async Task<QuestionBank?> TryGet(Guid questionBankId, CancellationToken token)
	{
		var proxy = await _context.QuestionBanks.FirstOrDefaultAsync(b => b.Id == questionBankId, token);
		return proxy?.QuestionBank;
	}

	public async Task UpdateAsync(QuestionBank bank, CancellationToken token)
	{
		var dbBank = await _context.QuestionBanks.FirstAsync(q => q.Id == bank.Id, token);
		dbBank.QuestionBank = bank;
	}
}
