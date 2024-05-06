using Concrete.Extensions.Quizes.Questions.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Concrete.Extensions.Quizes.Questions.Core.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExtensionController : ControllerBase
{
	[HttpGet("question-editor/{questionTypeName}")]
	public ActionResult<Uri> GetExtensionEditorUri(QuestionTypeName questionTypeName)
	{
		var url = $"{Request.Scheme}://{Request.Host}";
		if (questionTypeName == MultipleChoiceQuestion.TypeName)
			return Ok(new Uri(url + "/editor/multiple-choice-question"));
		return NotFound();
	}

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
