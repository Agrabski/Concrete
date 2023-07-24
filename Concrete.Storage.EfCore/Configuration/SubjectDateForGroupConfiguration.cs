using Concrete.Core.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concrete.Storage.EfCore.Configuration;
internal class SubjectDateForGroupConfiguration : IEntityTypeConfiguration<SubjectDateForGroup>
{
	public void Configure(EntityTypeBuilder<SubjectDateForGroup> builder)
	{
		builder.HasKey(x => x.GroupId);
		builder.Property(x => x.GroupId);
		builder.Property(x => x.Time);
	}
}
