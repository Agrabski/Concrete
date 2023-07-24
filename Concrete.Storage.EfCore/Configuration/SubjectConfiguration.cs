using Concrete.Core.Courses;
using Concrete.Core.Serialization;
using Concrete.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Concrete.Storage.EfCore.Configuration;

internal class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
	private readonly IConcreteSerializer _serializer;

	public SubjectConfiguration(IConcreteSerializer serializer)
	{
		_serializer = serializer;
	}

	public void Configure(EntityTypeBuilder<Subject> builder)
	{
		builder.Property(s => s.Id);
		builder.Property(s => s.TemplateId);
		builder.HasMany(s => s.DatesForGroups).WithOne().HasForeignKey("SubjectId");
		builder.Property(s => s.Name).HasConversion(
			s => JsonSerializer.Serialize(s, null as JsonSerializerOptions),
			s => JsonSerializer.Deserialize<LocalisedString>(s, null as JsonSerializerOptions)!);
		builder.Property(s => s.Description).HasConversion(
			s => JsonSerializer.Serialize(s, null as JsonSerializerOptions),
			s => JsonSerializer.Deserialize<LocalisedString>(s, null as JsonSerializerOptions)!);
		builder
			.Property(s => s.Activities)
			.HasConversion(
				a => _serializer.Serialize(a),
				s => _serializer.Deserialize<SubjectActivity[]>(s)
			);

		builder.HasKey(s => s.Id);

	}
}
