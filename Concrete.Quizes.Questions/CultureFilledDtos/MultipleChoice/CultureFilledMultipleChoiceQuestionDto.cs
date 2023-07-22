using Concrete.Core.Questions.CultureFilledDtos;

namespace Concrete.Quizes.Questions.CultureFilledDtos.MultipleChoice;
public record CultureFilledMultipleChoiceQuestionDto(
	string Question,
	CultureFilledMultipleChoiceAnswerDto[] Answers,
	Guid QuestionId
) : ICultureFilledQuestion;
