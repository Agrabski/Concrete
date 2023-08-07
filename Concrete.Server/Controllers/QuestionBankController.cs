using Concrete.Core;
using Concrete.Core.Services;
using Concrete.Core.Services.QuestionBanks;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class QuestionBankController : ControllerBase
{
	private readonly IQuestionBankRepository _questionBankRepository;
	private readonly IConcreteUnitOfWork _concreteUnitOfWork;

	public QuestionBankController(IQuestionBankRepository questionBankRepository, IConcreteUnitOfWork concreteUnitOfWork)
	{
		_questionBankRepository = questionBankRepository;
		_concreteUnitOfWork = concreteUnitOfWork;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<QuestionBank>> GetQuestionBankAsync(Guid id, CancellationToken token)
	{
		var result = await _questionBankRepository.TryGet(id, token);
		if (result is null)
			return NotFound();
		return Ok(result);
	}

	[HttpPost("create")]
	public async Task<ActionResult> CreateAsync([FromBody] QuestionBank bank, CancellationToken token)
	{
		await _questionBankRepository.AddAsync(bank, token);
		await _concreteUnitOfWork.CommitAsync(token);
		return Ok();
	}

	[HttpPost("update")]
	public async Task<ActionResult> UpdateAsync([FromBody] QuestionBank bank, CancellationToken token)
	{
		await _questionBankRepository.UpdateAsync(bank, token);
		await _concreteUnitOfWork.CommitAsync(token);
		return Ok();
	}
}
