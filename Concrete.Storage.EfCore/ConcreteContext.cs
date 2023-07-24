using Concrete.Core.Courses;
using Concrete.Core.Services;
using Concrete.Storage.EfCore.Configuration;
using Concrete.Storage.EfCore.Repos;
using Microsoft.EntityFrameworkCore;

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
	internal DbSet<QuestionBankProxy> QuestionBanks => Set<QuestionBankProxy>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		_configuration.Configure(modelBuilder);
		base.OnModelCreating(modelBuilder);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
#if DEBUG
		optionsBuilder.LogTo(Console.WriteLine);
		optionsBuilder.EnableSensitiveDataLogging();
#endif
		base.OnConfiguring(optionsBuilder);
	}

	public Task CommitAsync(CancellationToken token) => base.SaveChangesAsync(token);
}
