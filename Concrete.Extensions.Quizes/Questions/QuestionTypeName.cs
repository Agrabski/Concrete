using Concrete.Interface;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Concrete.Extensions.Quizes.Questions;
[JsonConverter(typeof(ParsableJsonConverter<QuestionTypeName>))]
public record struct QuestionTypeName(ExtensionName Extension, string Name) : IParsable<QuestionTypeName>
{
	public readonly override string ToString() => $"{Extension}::{Name}";
	public static QuestionTypeName Parse(string s, IFormatProvider? provider)
	{
		var parts = s.Split("::");
		return new(ExtensionName.Parse(parts[0], provider), parts[1]);
	}
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out QuestionTypeName result)
	{
		if (s?.Split("::") is [var extension, var name] && ExtensionName.TryParse(extension, provider, out var e))
		{
			result = new(e, name);
			return true;
		}
		result = default;
		return false;
	}
}
