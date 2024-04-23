using Concrete.Core.Template;
using Concrete.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concrete.Core.Data.EntityConfiguration.Templates;

internal class ConfigureActivityTemplate : IEntityTypeConfiguration<ActivityTemplate>
{
	public void Configure(EntityTypeBuilder<ActivityTemplate> builder)
	{
		builder.ToTable("ACTIVITY_TEMPLATE");

		builder.HasKey(t => t.Id);
		builder.Property(t => t.Id).ValueGeneratedNever();

		builder.OwnsOne(t => t.TemplateData).ToJson();
		builder.OwnsOne(t => t.DisplayName).ToJson();
		builder
			.Property(t => t.Name)
			.HasConversion(n => n.ToString(), s => ActivityName.Parse(s, null))
			.HasMaxLength(512);
	}
}
