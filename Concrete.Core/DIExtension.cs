using Concrete.Core.Questions.Instances;
using Concrete.Core.Questions.Templates;
using Concrete.Core.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Core;

public static class DIExtension
{
	public static IServiceCollection AddConcrete(this IServiceCollection collection) => collection
		.AddSingleton<IConcreteSerializer, ConcreteSerializer>();

	public static IServiceCollection AddQuestionType
		<
		TQuestionTemplate,
		TQuestion,
		TQuestionAnswer
		>
		(this IServiceCollection collection, string? discriminator = null)
		where TQuestionTemplate : IQuestionTemplate<TQuestionAnswer>
		where TQuestion : IQuestion<TQuestionAnswer>
		where TQuestionAnswer : IQuestionAnswer => collection
			.AddSingleton(_ => PolymorphicTypeInfo<IQuestion>.FromImplementation<TQuestion>(discriminator))
			.AddSingleton(_ => PolymorphicTypeInfo<IQuestionTemplate>.FromImplementation<TQuestionTemplate>(discriminator))
			.AddSingleton(_ => PolymorphicTypeInfo<IQuestionAnswer>.FromImplementation<TQuestionAnswer>(discriminator));

}
