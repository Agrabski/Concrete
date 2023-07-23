using Concrete.Core;

namespace Concrete.Quizes.Questions.Instances.MultipleChoice;

public record struct MultipleChoiceQuestionAnswerSet(int[] AnswerIndicies) : IQuestionAnswer;
