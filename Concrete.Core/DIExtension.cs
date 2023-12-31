﻿using Concrete.Core.Activities.Instances;
using Concrete.Core.Activities.Templates;
using Concrete.Core.Questions.Instances;
using Concrete.Core.Questions.Templates;
using Concrete.Core.Serialization;
using Concrete.Core.Services;
using Concrete.Core.Services.Activities;
using Concrete.Core.Services.Courses;
using Concrete.Core.Services.QuestionBanks;
using Concrete.Core.Services.Subjects;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Urbanite;

[assembly: InternalsVisibleTo("Concrete.Core.Tests")]

namespace Concrete.Core;

public static class DIExtension
{
	public static IServiceCollection AddConcrete(this IServiceCollection collection) => collection
		.AddUrbanite()
		.AddSingleton<IConcreteSerializer, ConcreteSerializer>()
		.AddTransient<ICourseService, CourseService>()
		.AddTransient<IQuizService, QuizService>()
		.AddActivityType<QuizTemplate, QuizInstance>(ConcreteConvetion.TypeDiscriminator("Concrete", "Core", "Quiz"))
		.AddScoped<IStudentGroupService, StudentGroupService>()
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
			.AddUrbaniteSerializableType<IQuestion, TQuestion>(discriminator)
			.AddUrbaniteSerializableType<IQuestionTemplate, TQuestionTemplate>(discriminator)
			.AddUrbaniteSerializableType<IQuestionAnswer, TQuestionAnswer>(discriminator)
			.AddUrbaniteSerializableType<IQuestionGradingResponse, ManualGradingResponse<TQuestionAnswer>>(discriminator)
			;


	public static IServiceCollection AddActivityType
	<
		TActivityTemplate,
		TActivity
	>
	(this IServiceCollection collection, string? discriminator = null)
		where TActivityTemplate : IActivityTemplate
		where TActivity : IActivity => collection
			.AddUrbaniteSerializableType<IActivityTemplate, TActivityTemplate>(discriminator)
			.AddUrbaniteSerializableType<IActivity, TActivity>(discriminator)
			;


	public static IServiceCollection AddConcreteStorageImplementation
		<
			TQuestionBankRepository,
			TSubjectRepository,
			TCourseRepository,
			TStudentGroupRepository,
			TActivityInstanceRepository
		>(this IServiceCollection collection)
			where TQuestionBankRepository : class, IQuestionBankRepository
			where TSubjectRepository : class, ISubjectRepository
			where TCourseRepository : class, ICourseRepository
			where TStudentGroupRepository : class, IStudentGroupRepository
			where TActivityInstanceRepository : class, IActivityInstanceRepository
		=> collection
		.AddScoped<IQuestionBankRepository, TQuestionBankRepository>()
		.AddScoped<ISubjectRepository, TSubjectRepository>()
		.AddScoped<ICourseRepository, TCourseRepository>()
		.AddScoped<IStudentGroupRepository, TStudentGroupRepository>()
		.AddScoped<IActivityInstanceRepository, TActivityInstanceRepository>()
		;


}
