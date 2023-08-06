using Concrete.Core;
using Concrete.Core.Services.QuestionBanks;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class QuestionBankController : ControllerBase
{
	private readonly IQuestionBankRepository _questionBankRepository;

	public QuestionBankController(IQuestionBankRepository questionBankRepository)
	{
		_questionBankRepository = questionBankRepository;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<QuestionBank>> GetQuestionBankAsync(Guid id, CancellationToken token)
	{
		var result = await _questionBankRepository.TryGet(id, token);
		if (result is null)
			return NotFound();
		return Ok(result);
	}
}
