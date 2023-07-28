using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concrete.Storage.EfCore.Configuration;

internal class AuthenticatedUserConfiguration : IEntityTypeConfiguration<DatabaseUser>
{
	public void Configure(EntityTypeBuilder<DatabaseUser> builder)
	{
		builder.OwnsOne(u => u.AuthenticationInfo, b =>
		{
			b.Property(i => i.Email);
			b.Property(i => i.PasswordHash);
		});
	}
}
