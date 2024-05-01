using Concrete.Serialization;
using System.Text.Json;

namespace Concrete.Extensions.Quizes.Questions.Template;
public sealed class QuestionTemplate : IPolymorphicDataHolder<QuestionTypeName>
{
	public required string Name { get; set; }
	public Guid Id { get; init; } 
	public QuestionTypeName Discriminator { get; set; }
	public required JsonDocument Data { get; set; }
}

