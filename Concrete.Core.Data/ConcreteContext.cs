using Concrete.Core.Data.EntityConfiguration.Templates;
using Concrete.Core.Template;
using Microsoft.EntityFrameworkCore;

namespace Concrete.Core.Data;

public class ConcreteContext(DbContextOptions<ConcreteContext> options) : DbContext(options)
{
	public DbSet<CourseTemplate> CourseTemplates => Set<CourseTemplate>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.ApplyConfiguration(new ConfigureActivityTemplate())
			.ApplyConfiguration(new ConfigureClassTemplate())
			.ApplyConfiguration(new ConfigureCourseTemplate())
			;
	}
}
