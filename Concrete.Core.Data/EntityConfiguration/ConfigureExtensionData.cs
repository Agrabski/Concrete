using Concrete.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Concrete.Core.Data.EntityConfiguration;

internal class ConfigureExtensionData : IEntityTypeConfiguration<ExtensionData>
{
	public void Configure(EntityTypeBuilder<ExtensionData> builder)
	{
		builder.HasKey(e => new { e.Key, e.ExtensionName });
		builder.HasIndex(e => e.Category);
		builder.HasIndex(e => e.Modified);
		builder.Property(e => e.Key).HasMaxLength(256);
		builder.Property(e => e.ExtensionName).HasMaxLength(256).HasConversion(e => e.ToString(), s => ExtensionName.Parse(s, null));
		builder.Property(e => e.Category).HasMaxLength(256);
		builder.Property(e => e.Modified);
		builder
			.Property(e => e.Value)
			.HasConversion(
			e => e.RootElement.GetRawText(),
			e => JsonSerializer.Deserialize<JsonDocument>(e, JsonSerializerOptions.Default)!
		);
	}
}
