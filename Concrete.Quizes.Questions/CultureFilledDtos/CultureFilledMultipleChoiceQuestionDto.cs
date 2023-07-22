using Concrete.Core.Questions.CultureFilledDtos;

namespace Concrete.Quizes.Questions.CultureFilledDtos;
public record CultureFilledMultipleChoiceQuestionDto(
	string Question,
	CultureFilledMultipleChoiceAnswerDto[] Answers,
	Guid QuestionId
) : ICultureFilledQuestion;
