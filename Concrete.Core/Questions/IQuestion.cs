namespace Concrete.Core.Questions;
public interface IQuestion
{
	public float Score(object answer);
}
public interface IQuestion<TAnswer> : IQuestion
{
	float IQuestion.Score(object answer)
	{
		if (answer is TAnswer a)
			return Score(a);
		throw new ArgumentException($"Expected {typeof(TAnswer)} but got {answer.GetType()}");
	}
	public float Score(TAnswer answer);
}
