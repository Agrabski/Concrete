using Concrete.Core;

namespace Concrete.Quizes.Questions.Instances.Open;

public record struct OpenQuestionAnswer(string Answer) : IQuestionAnswer;
