using Concrete.Core;
using Concrete.Core.Serialization;
using Concrete.Storage.EfCore.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concrete.Storage.EfCore.Configuration;

internal class QuestionBankConfiguration : IEntityTypeConfiguration<QuestionBankProxy>
{
	private readonly IConcreteSerializer _concreteSerializer;
	public QuestionBankConfiguration(IConcreteSerializer concreteSerializer)
	{
		_concreteSerializer = concreteSerializer;
	}

	public void Configure(EntityTypeBuilder<QuestionBankProxy> builder)
	{
		builder.Property(p => p.Id);
		builder
			.Property(p => p.QuestionBank)
			.HasConversion(
				q => _concreteSerializer.Serialize(q),
				s => _concreteSerializer.Deserialize<QuestionBank>(s)
			);
	}
}
