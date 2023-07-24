﻿using Concrete.Core.Activities.Instances;
using Concrete.Core.Activities.Templates;
using Concrete.Core.Questions.Instances;
using Concrete.Core.Questions.Templates;
using Concrete.Core.Serialization;
using Concrete.Core.Services.Activities;
using Concrete.Core.Services.Courses;
using Concrete.Core.Services.QuestionBanks;
using Concrete.Core.Services.Subjects;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization.Metadata;

namespace Concrete.Core;

public static class DIExtension
{
	public static IServiceCollection AddConcrete(this IServiceCollection collection) => collection
		.AddSingleton<IConcreteSerializer, ConcreteSerializer>()
		.AddTransient<ICourseService, CourseService>()
		.AddTransient<IQuizService, QuizService>()
		.AddSingleton<DefaultJsonTypeInfoResolver, PolymorphicTypeResolver>()
		.AddActivityType<QuizTemplate, QuizInstance>(ConcreteConvetion.TypeDiscriminator("Concrete", "Core", "Quiz"))
		;

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


	public static IServiceCollection AddActivityType
	<
		TActivityTemplate,
		TActivity
	>
	(this IServiceCollection collection, string? discriminator = null)
		where TActivityTemplate : IActivityTemplate
		where TActivity : IActivity => collection
			.AddSingleton(_ => PolymorphicTypeInfo<IActivityTemplate>.FromImplementation<TActivityTemplate>(discriminator))
			.AddSingleton(_ => PolymorphicTypeInfo<IActivity>.FromImplementation<TActivity>(discriminator))
			;


	public static IServiceCollection AddStorageImplementation
		<
			TQuestionBankRepository,
			TSubjectRepository,
			TCourseRepository
		>(this IServiceCollection collection)
			where TQuestionBankRepository : class, IQuestionBankRepository
			where TSubjectRepository : class, ISubjectRepository
			where TCourseRepository : class, ICourseRepository
		=> collection
		.AddScoped<IQuestionBankRepository, TQuestionBankRepository>()
		.AddScoped<ISubjectRepository, TSubjectRepository>()
		.AddScoped<ICourseRepository, TCourseRepository>();


}
