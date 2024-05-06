using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Extensions.Quizes.Questions.Client;

public static class DIExtension
{
	public static IServiceCollection AddQuestionsClient(this IServiceCollection services, Action<QuestionsClientConfiguration> configure)
	{
		return services
			.AddOptions<QuestionsClientConfiguration>()
			.Configure(configure)
			.Services
			.AddTransient<IQuestionExtenionsClient, QuestionsClient>()
			.AddHttpClient<IQuestionExtenionsClient, QuestionsClient>()
			.Services
			;
	}
}
