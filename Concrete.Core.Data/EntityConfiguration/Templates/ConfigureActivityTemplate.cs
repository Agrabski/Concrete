using Concrete.Core.Template;
using Concrete.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Concrete.Core.Data.EntityConfiguration.Templates;

internal class ConfigureActivityTemplate : IEntityTypeConfiguration<ActivityTemplate>
{
	public void Configure(EntityTypeBuilder<ActivityTemplate> builder)
	{
		builder.ToTable("ACTIVITY_TEMPLATE");

		builder.HasKey(t => t.Id);
		builder.Property(t => t.Id).ValueGeneratedNever();

		builder.Property(t => t.Name).HasMaxLength(512);
		builder.Property(t => t.Data).HasConversion(
			d => d.RootElement.GetRawText(),
			s => JsonDocument.Parse(s, new JsonDocumentOptions())
		);
		builder.OwnsOne(t => t.DisplayName).ToJson();
		builder
			.Property(t => t.Discriminator)
			.HasConversion(n => n.ToString(), s => ActivityTypeName.Parse(s, null))
			.HasMaxLength(512);
	}
}
