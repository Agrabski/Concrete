using Concrete.Core.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concrete.Core.Data.EntityConfiguration.Templates;

internal class ConfigureClassTemplate : IEntityTypeConfiguration<ClassTemplate>
{
	public void Configure(EntityTypeBuilder<ClassTemplate> builder)
	{
		builder.ToTable("CLASS_TEMPLATE");
		builder.HasKey(t => t.Id);

		builder.HasMany(t => t.ActivityTemplates).WithOne();
		builder.Property(t => t.Name).HasMaxLength(512);
	}
}
