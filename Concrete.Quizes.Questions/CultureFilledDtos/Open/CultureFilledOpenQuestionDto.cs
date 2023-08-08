using Concrete.Core.Questions.CultureFilledDtos;

namespace Concrete.Quizes.Questions.CultureFilledDtos.Open;
public record CultureFilledOpenQuestionDto(string Question, Guid QuestionId) : ICultureFilledQuestion;
