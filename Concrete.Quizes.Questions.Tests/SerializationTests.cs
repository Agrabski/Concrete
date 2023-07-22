using Concrete.Core;
using Concrete.Core.Serialization;
using Concrete.Quizes.Questions.Templates.MultipleChoice;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Quizes.Questions.Tests;

public class SerializationTests
{
	private readonly IServiceProvider _serviceProvider = new ServiceCollection()
		.AddConcrete()
		.AddBuiltInConcreteQuestions()
		.BuildServiceProvider();
	[Fact]
	public void Test1()
	{
		var serializer = _serviceProvider.GetRequiredService<IConcreteSerializer>();
		var text = serializer.Serialize(new MultipleChoiceQuestionTemplate()
		{
			Question = new(new()
			{
				["pl"] = "Czemu jesteś biały?",
				["en"] = "Why are you white?"
			}),
			ParameterNames = new(),
			Answers = new()
			{
				new(0, new(new(){["pl"] = "Bo tak", ["en"] = "Because"})),
				new(1, new(new(){["pl"] = "?!", ["en"] = "?1"})),
				new(2, new(new(){["pl"] = "Nie wiem", ["en"] = "I dont know"})),
			},
		});
		text.Trim();
	}
}
