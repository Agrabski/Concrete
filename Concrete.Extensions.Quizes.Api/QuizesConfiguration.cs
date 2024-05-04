namespace Concrete.Extensions.Quizes.Api;

public sealed class QuizesConfiguration
{
	public Uri ActivityEditorUri { get; set; } = new("http://quizes-editor");
	public Uri QuestionsMenuUri => new(ActivityEditorUri, "questions");
}
