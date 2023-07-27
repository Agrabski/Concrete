using Concrete.Core.Courses;
using Concrete.Storage.EfCore.Repos;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Storage.EfCore.Configuration;

internal class ConcreteContextConfiguration
{
	private readonly IEntityTypeConfiguration<CourseTemplateProxy> _courseTemplate;
	private readonly IEntityTypeConfiguration<Subject> _subject;
	private readonly IEntityTypeConfiguration<QuestionBankProxy> _questionBank;
	private readonly IEntityTypeConfiguration<ActivityInstanceProxy> _activityInstance;
	public ConcreteContextConfiguration(
		IEntityTypeConfiguration<CourseTemplateProxy> courseTemplate,
		IEntityTypeConfiguration<Subject> subject,
		IEntityTypeConfiguration<QuestionBankProxy> questionBank,
		IEntityTypeConfiguration<ActivityInstanceProxy> activityInstance)
	{
		_courseTemplate = courseTemplate;
		_subject = subject;
		_questionBank = questionBank;
		_activityInstance = activityInstance;
	}

	internal void Configure(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(_courseTemplate);
		modelBuilder.ApplyConfiguration(_subject);
		modelBuilder.ApplyConfiguration(_questionBank);
		modelBuilder.ApplyConfiguration(_activityInstance);
	}
}
