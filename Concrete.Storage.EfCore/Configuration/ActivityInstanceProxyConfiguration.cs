using Concrete.Core.Activities.Instances;
using Concrete.Core.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concrete.Storage.EfCore.Configuration;
internal class ActivityInstanceProxyConfiguration : IEntityTypeConfiguration<ActivityInstanceProxy>
{
	private readonly IConcreteSerializer _serializer;

	public ActivityInstanceProxyConfiguration(IConcreteSerializer serializer)
	{
		_serializer = serializer;
	}
	public void Configure(EntityTypeBuilder<ActivityInstanceProxy> builder)
	{
		builder.Property(p => p.Id);
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Instance).HasConversion(
			i => _serializer.Serialize(i),
			i => _serializer.Deserialize<IActivity>(i)
		);
	}
}
