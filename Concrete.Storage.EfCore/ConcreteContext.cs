using Concrete.Core;
using Concrete.Core.Courses;
using Concrete.Core.Services;
using Concrete.Storage.EfCore.Configuration;
using Concrete.Storage.EfCore.Repos;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Concrete.Storage.EfCore;
internal class ConcreteContext : DbContext, IConcreteUnitOfWork
{
	private readonly ConcreteContextConfiguration _configuration;
	public ConcreteContext(DbContextOptions<ConcreteContext> options, ConcreteContextConfiguration configuration) : base(options)
	{
		_configuration = configuration;
	}
	internal DbSet<Course> Courses => Set<Course>();
	internal DbSet<CourseTemplateProxy> CourseTemplates => Set<CourseTemplateProxy>();
	internal DbSet<Subject> Subjects => Set<Subject>();
	internal DbSet<ActivityInstanceProxy> ActivityInstances => Set<ActivityInstanceProxy>();
	internal DbSet<QuestionBankProxy> QuestionBanks => Set<QuestionBankProxy>();
	internal DbSet<StudentGroup> StudentGroups => Set<StudentGroup>();
	internal DbSet<DatabaseUser> Users => Set<DatabaseUser>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		_configuration.Configure(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		base.OnModelCreating(modelBuilder);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
#if DEBUG
		optionsBuilder
			.LogTo(m => Debug.WriteLine(m), new[] { DbLoggerCategory.Database.Command.Name })
			.EnableSensitiveDataLogging()
			.EnableDetailedErrors();
#endif
		base.OnConfiguring(optionsBuilder);
	}

	public Task CommitAsync(CancellationToken token) => base.SaveChangesAsync(token);
}
