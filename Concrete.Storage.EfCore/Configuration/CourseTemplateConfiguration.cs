using Concrete.Core.Courses;
using Concrete.Core.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concrete.Storage.EfCore.Configuration;
internal class CourseTemplateConfiguration : IEntityTypeConfiguration<CourseTemplateProxy>
{
	private readonly IConcreteSerializer _serializer;

	public CourseTemplateConfiguration(IConcreteSerializer serializer)
	{
		_serializer = serializer;
	}

	public void Configure(EntityTypeBuilder<CourseTemplateProxy> builder)
	{
		builder.Property(p => p.Id);
		builder.Property(p => p.Template)
			.HasConversion(
			t => _serializer.Serialize(t),
			s => _serializer.Deserialize<CourseTemplate>(s)
		);

		builder.HasKey(p => p.Id);
	}
}
