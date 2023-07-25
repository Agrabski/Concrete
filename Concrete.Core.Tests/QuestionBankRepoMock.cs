using Concrete.Core.Services.QuestionBanks;

namespace Concrete.Core.Tests;

internal class QuestionBankRepoMock : IQuestionBankRepository
{
	private readonly Dictionary<Guid, IQuestionBank> _questionBanks;
	public QuestionBankRepoMock(Dictionary<Guid, IQuestionBank> questionBanks)
	{
		_questionBanks = questionBanks;
	}

	public Task<IQuestionBank?> TryGet(Guid questionBankId, CancellationToken token)
	{
		if (_questionBanks.TryGetValue(questionBankId, out var questionBank))
			return Task.FromResult<IQuestionBank?>(questionBank);
		return Task.FromResult<IQuestionBank?>(null);
	}
}
