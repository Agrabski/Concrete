using Concrete.Core.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concrete.Core.Data.EntityConfiguration.Templates;
internal class ConfigureCourseTemplate : IEntityTypeConfiguration<CourseTemplate>
{
	public void Configure(EntityTypeBuilder<CourseTemplate> builder)
	{
		builder.ToTable("COURSE_TEMPLATE");
		builder.HasKey(t => t.Id);
		builder.Property(t => t.Id).ValueGeneratedNever();

		builder.Property(t => t.Name).HasMaxLength(512);
		builder.HasMany(t => t.ClassTemplates).WithOne();
	}
}
