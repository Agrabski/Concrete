using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Extensions.Quizes.Questions.Client;

public static class DIExtension
{
	public static IServiceCollection AddQuestionsClient(this IServiceCollection services)
	{
		return services
			.AddTransient<IQuestionsClient, QuestionsClient>()
			.AddHttpClient<IQuestionsClient, QuestionsClient>()
			.Services
			;
	}
}
