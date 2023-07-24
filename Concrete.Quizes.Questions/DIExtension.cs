using Concrete.Core;
using Concrete.Quizes.Questions.Instances.MultipleChoice;
using Concrete.Quizes.Questions.Templates.MultipleChoice;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Quizes.Questions;
public static class DIExtension
{
	public static IServiceCollection AddBuiltInConcreteQuestions(this IServiceCollection collection) => collection
		.AddQuestionType<
			MultipleChoiceQuestionTemplate,
			MultipleChoiceQuestion,
			MultipleChoiceQuestionAnswerSet
			>(ConcreteConvetion.TypeDiscriminator("Concrete", "Basics", "MultipleChoice"));
}
