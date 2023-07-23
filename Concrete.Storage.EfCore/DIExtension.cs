using Concrete.Core;
using Concrete.Storage.EfCore.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Storage.EfCore;
public static class DIExtension
{
	public static IServiceCollection AddConcreteEfCoreStorage(this IServiceCollection services) => services
		.AddStorageImplementation<
			EfCoreQuestionBankRepository,
			EfCoreSubjectRepository,
			EfCoreCourseRepository>()
		.AddDbContext<ConcreteContext>();
}
