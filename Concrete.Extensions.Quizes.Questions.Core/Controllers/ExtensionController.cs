using Concrete.Extensions.Quizes.Questions.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Extensions.Quizes.Questions.Core.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExtensionController : ControllerBase
{
	[HttpGet("question-types")]
	public QuestionTypeName[] GetQuestionTypeNames()
	{
		return
		[
			MultipleChoiceQuestion.TypeName
		];
	}

	[HttpGet("{typeName}")]
	public ActionResult<ICoreQuestion> GetQuestionInstance(QuestionTypeName typeName)
	{
		if (typeName == MultipleChoiceQuestion.TypeName)
			return Ok(new MultipleChoiceQuestion());
		return NotFound();
	}
}
