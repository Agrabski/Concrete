﻿using Concrete.Core;
using Concrete.Core.Courses;
using Concrete.Core.Services;
using Concrete.Storage.EfCore.Configuration;
using Concrete.Storage.EfCore.Repos;
using Concrete.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Storage.EfCore;
public static class DIExtension
{
	public static IServiceCollection AddConcreteEfCoreStorage(this IServiceCollection services, bool includeDatabaseAuthenticationData, Action<DbContextOptionsBuilder>? optionsBuilder)
	{
		if (includeDatabaseAuthenticationData)
		{
			services
				.AddSingleton<IEntityTypeConfiguration<DatabaseUser>, AuthenticatedUserConfiguration>()
				.AddScoped<IAuthenticationInfoRepository, UserRepository>();
		}

		return services
		.AddConcreteStorageImplementation<
			EfCoreQuestionBankRepository,
			EfCoreSubjectRepository,
			EfCoreCourseRepository,
			EfCoreStudentGroupRepository,
			EfCoreActivityInstanceRepository>()
		.AddDbContext<ConcreteContext>(optionsBuilder)
		.AddSingleton<IEntityTypeConfiguration<User>, DatabaseUserConfiguration>()
		.AddSingleton<IEntityTypeConfiguration<CourseTemplateProxy>, CourseTemplateConfiguration>()
		.AddSingleton<IEntityTypeConfiguration<Subject>, SubjectConfiguration>()
		.AddSingleton<IEntityTypeConfiguration<QuestionBankProxy>, QuestionBankConfiguration>()
		.AddSingleton<IEntityTypeConfiguration<ActivityInstanceProxy>, ActivityInstanceProxyConfiguration>()
		.AddSingleton<ConcreteContextConfiguration>()
		.AddScoped<IConcreteUnitOfWork>(s => s.GetRequiredService<ConcreteContext>())
		.AddScoped<IConcreteMigrator, ConcreteMigrator>()
		.AddConcreteUsers<UserRepository>()
		;
	}
}
