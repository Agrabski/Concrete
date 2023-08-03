using Concrete.Core;
using Concrete.Core.Activities.Templates;
using Concrete.Core.Courses;
using Concrete.Core.Serialization;
using Concrete.Quizes.Questions.Instances.MultipleChoice.Grading;
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
	public void CourseTemplateWithMultipleChoiceQuestionCanBeSerailzedAndDeserialized()
	{
		var serializer = _serviceProvider.GetRequiredService<IConcreteSerializer>();
		var courseTemplate = new CourseTemplate()
		{
			TemplateName = "test",
			TemplateId = Guid.NewGuid(),
			Subjects =
			{
				new(
					new[]
					{
						new SubjectActivity()
						{
							Id= Guid.NewGuid(),
							Template = new QuizTemplate()
							{
								Questions =
								{
									new QuizTemplateSingleQuestionReference(Guid.NewGuid(), Guid.NewGuid(), new AllQuestionVariantsTemplateFilingMode())
								}
							}
						}
					}, new(), new(), Guid.NewGuid(), "test")
			}
		};
		var text = serializer.Serialize(courseTemplate);
		text.Trim();
	}

	[Fact]
	public void QuestionBankWithMultipleChoiceQuestionCanBeSerializedAndDeserialized()
	{
		var serializer = _serviceProvider.GetRequiredService<IConcreteSerializer>();
		var question = new MultipleChoiceQuestionTemplate()
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
			Grading = new AllOrNothingGrading(0)
		};
		var questionBank = new QuestionBank()
		{
			QuestionTemplates = { question }
		};
		var text = serializer.Serialize(questionBank);


		var deserializedBank = serializer.Deserialize<QuestionBank>(text);
		Assert.Collection(
			deserializedBank.QuestionTemplates,
			q => Assert.Collection(
				Assert.IsType<MultipleChoiceQuestionTemplate>(q).Answers,
				_ => { },
				_ => { },
				_ => { }
				)
		);
	}
}
