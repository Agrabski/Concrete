using Concrete.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concrete.Storage.EfCore.Configuration;

internal class DatabaseUserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id);
		builder.Property(x => x.Name).IsRequired();
		builder.Property(x => x.Surname).IsRequired();
		builder.Property(x => x.Role).HasConversion<int>();
	}
}
