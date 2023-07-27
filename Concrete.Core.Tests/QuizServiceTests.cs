using Concrete.Core.Activities.Templates;
using Concrete.Core.Courses;
using Concrete.Core.Questions.Templates;
using Concrete.Core.Services;
using Concrete.Core.Services.Activities;
using Concrete.Core.Services.Courses;
using Concrete.Core.Services.QuestionBanks;
using Concrete.Core.Services.Subjects;
using Concrete.Quizes.Questions;
using Concrete.Quizes.Questions.Instances.MultipleChoice.Grading;
using Concrete.Quizes.Questions.Templates.MultipleChoice;
using Concrete.Storage.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Core.Tests;

public class QuizServiceTests : IDisposable
{

	private static IServiceProvider BuildServiceProvider()
	{
		Directory.CreateDirectory("data");
		var result = new ServiceCollection()
			.AddConcrete()
			.AddConcreteEfCoreStorage(db => db.UseSqlite($"Data Source=data\\{Guid.NewGuid()}.db"))
			.AddBuiltInConcreteQuestions()
			.BuildServiceProvider();
		return result;
	}

	public void Dispose() => Directory.Delete("data", true);

	[Fact]
	public async Task Test1Async()
	{
		var serviceProvider = BuildServiceProvider();
		using var scope = serviceProvider.CreateScope();
		await scope.ServiceProvider.GetRequiredService<IConcreteMigrator>().EnsureCreatedAsync(CancellationToken.None);

		var quizService = scope.ServiceProvider.GetRequiredService<IQuizService>();
		var subjectRepository = scope.ServiceProvider.GetRequiredService<ISubjectRepository>();
		var courseId = Guid.NewGuid();
		var subjectId = Guid.NewGuid();
		var activityId = Guid.NewGuid();
		var questionBankId = Guid.NewGuid();
		var questionId = Guid.NewGuid();
		var questionBank = new QuestionBank()
		{
			Id = questionBankId,
			QuestionTemplates =
			{
				new MultipleChoiceQuestionTemplate
				{
					Grading = new AllOrNothingGrading(0),
					Answers =
					{
						new(0, new()),
						new(1, new())
					},
					AvailableInstances = new QuestionTemplateInstance[]
					{
						new()
						{
							Id = Guid.NewGuid(),
							Parameters = new(),
							QuestionBankId = questionBankId,
							QuestionTemplateId = questionBankId,
						}
					},
					Question = new(),
					Id = questionId
				}
			}
		};
		await scope.ServiceProvider
			.GetRequiredService<IQuestionBankRepository>()
			.AddAsync(questionBank, CancellationToken.None);
		await scope.ServiceProvider.GetRequiredService<ICourseRepository>().AddAsync(new Course()
		{
			CourseCode = "test",
			Id = courseId,
			Name = "test",
		}, CancellationToken.None);
		await subjectRepository.AddSubjectAsync(new Subject
		{
			CourseId = courseId,
			Id = subjectId,
			Name = new(new() { ["pl"] = "test" }),
			Description = new(new() { ["pl"] = "test" }),
			DatesForGroups = new() { new SubjectDateForGroup(new(2020, 03, 04), Guid.NewGuid()) },
			Activities = new SubjectActivity[]
			{
				new(){
					Id=activityId,
					Template = new QuizTemplate()
					{
						Questions =
						{
							new QuizTemplateQuestionReference(questionBankId, questionId, new AllQuestionVariantsTemplateFilingMode())
						}
					}
				}
			}

		}, CancellationToken.None);

		await scope.ServiceProvider.GetRequiredService<IConcreteUnitOfWork>().CommitAsync(CancellationToken.None);

		var userId = Guid.NewGuid();
		var instanceId = await quizService.StartQuizAttemptAsync(userId, courseId, subjectId, activityId, CancellationToken.None);

		await scope.ServiceProvider.GetRequiredService<IConcreteUnitOfWork>().CommitAsync(CancellationToken.None);
	}
}
