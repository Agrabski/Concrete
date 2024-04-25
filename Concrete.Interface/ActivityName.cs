using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Concrete.Interface;
[type: JsonConverter(typeof(ParsableJsonConverter<ActivityTypeName>))]
public record struct ActivityTypeName(ExtensionName Extension, string Name) : IParsable<ActivityTypeName>
{
	public override readonly string ToString() => $"{Extension}::{Name}";
	public static ActivityTypeName Parse(string s, IFormatProvider? provider)
	{
		var parts = s.Split("::");
		return new(ExtensionName.Parse(parts[0], provider), parts[1]);
	}
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ActivityTypeName result)
	{
		if (s?.Split("::") is [var extension, var name] && ExtensionName.TryParse(extension, provider, out var parsedExtension))
		{
			result = new(parsedExtension, name);
			return true;
		}
		result = default;
		return false;
	}
}
