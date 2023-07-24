using Concrete.Core;
using Concrete.Core.Courses;
using Concrete.Core.Services;
using Concrete.Storage.EfCore.Configuration;
using Concrete.Storage.EfCore.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Storage.EfCore;
public static class DIExtension
{
	public static IServiceCollection AddConcreteEfCoreStorage(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsBuilder) => services
		.AddStorageImplementation<
			EfCoreQuestionBankRepository,
			EfCoreSubjectRepository,
			EfCoreCourseRepository>()
		.AddDbContext<ConcreteContext>(optionsBuilder)
		.AddSingleton<IEntityTypeConfiguration<CourseTemplateProxy>, CourseTemplateConfiguration>()
		.AddSingleton<IEntityTypeConfiguration<Subject>, SubjectConfiguration>()
		.AddSingleton<IEntityTypeConfiguration<QuestionBankProxy>, QuestionBankConfiguration>()
		.AddSingleton<ConcreteContextConfiguration>()
		.AddScoped<IConcreteUnitOfWork>(s => s.GetRequiredService<ConcreteContext>())
		;
}
