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

	public Task AddAsync(IQuestionBank bank, CancellationToken token) => _context.QuestionBanks.AddAsync(new(bank.Id, bank), token).AsTask();

	public async Task<IQuestionBank?> TryGet(Guid questionBankId, CancellationToken token)
	{
		var proxy = await _context.QuestionBanks.FirstOrDefaultAsync(b => b.Id == questionBankId, token);
		return proxy?.QuestionBank;
	}
}
