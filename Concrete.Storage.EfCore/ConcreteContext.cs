using Concrete.Core.Courses;
using Concrete.Storage.EfCore.Repos;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Storage.EfCore;
internal class ConcreteContext : DbContext
{
	internal DbSet<Course> Courses => Set<Course>();
	internal DbSet<CourseTemplate> CourseTemplates => Set<CourseTemplate>();
	internal DbSet<Subject> Subjects => Set<Subject>();
	internal DbSet<QuestionBankProxy> QuestionBanks => Set<QuestionBankProxy>();
}
