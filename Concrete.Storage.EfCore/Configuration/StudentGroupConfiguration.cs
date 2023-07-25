using Concrete.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concrete.Storage.EfCore.Configuration;

internal class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
{
	public void Configure(EntityTypeBuilder<StudentGroup> builder)
	{
		builder.Property(g => g.Id);
		builder.Property(g => g.Name);
		builder.HasMany(g => g.Users).WithMany();
		builder.HasKey(g => g.Id);
	}
}
