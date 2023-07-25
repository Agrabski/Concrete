using Concrete.Core.Services.Activities;
using Concrete.Storage.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Core.Tests;

public class QuizServiceTests
{

	private static IServiceProvider BuildServiceProvider()
	{
		var result = new ServiceCollection()
			.AddConcrete()
			.AddConcreteEfCoreStorage(db => db.UseSqlite("Data Source=InMemorySample;Mode=Memory;Cache=Shared"))
			.BuildServiceProvider();
		return result;
	}

	[Fact]
	public async Task Test1Async()
	{
		var serviceProvider = BuildServiceProvider();
		using var scope = serviceProvider.CreateScope();
		await scope.ServiceProvider.GetRequiredService<IConcreteMigrator>().EnsureCreatedAsync(CancellationToken.None);
		var quizService = scope.ServiceProvider.GetRequiredService<IQuizService>();

	}
}
