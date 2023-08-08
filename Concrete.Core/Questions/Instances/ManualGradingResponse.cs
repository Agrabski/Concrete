namespace Concrete.Core.Questions.Instances;

public record struct ManualGradingResponse<TAnswer>(TAnswer Answer, int? Grade) : IQuestionGradingResponse;

