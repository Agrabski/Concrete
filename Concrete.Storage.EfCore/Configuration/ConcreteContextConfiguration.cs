using Concrete.Core.Courses;
using Concrete.Storage.EfCore.Repos;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Storage.EfCore.Configuration;
internal class ConcreteContextConfiguration
{
	private readonly IEntityTypeConfiguration<CourseTemplateProxy> _courseTemplate;
	private readonly IEntityTypeConfiguration<Subject> _subject;
	private readonly IEntityTypeConfiguration<QuestionBankProxy> _questionBank;
	public ConcreteContextConfiguration(
		IEntityTypeConfiguration<CourseTemplateProxy> courseTemplate,
		IEntityTypeConfiguration<Subject> subject,
		IEntityTypeConfiguration<QuestionBankProxy> questionBank)
	{
		_courseTemplate = courseTemplate;
		_subject = subject;
		_questionBank = questionBank;
	}

	internal void Configure(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CourseTemplateProxy>(_courseTemplate.Configure);
		modelBuilder.Entity<Subject>(_subject.Configure);
		modelBuilder.Entity<QuestionBankProxy>(_questionBank.Configure);
		modelBuilder.Entity<SubjectDateForGroup>(new SubjectDateForGroupConfiguration().Configure);
	}
}
