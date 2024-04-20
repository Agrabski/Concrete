using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Concrete.Interface;
[JsonConverter(typeof(ParsableJsonConverter<ActivityName>))]
public record struct ActivityName(ExtensionName Extension, string Name) : IParsable<ActivityName>
{
	public override readonly string ToString() => $"{Extension}::{Name}";
	public static ActivityName Parse(string s, IFormatProvider? provider)
	{
		var parts = s.Split("::");
		return new(ExtensionName.Parse(parts[0], provider), parts[1]);
	}
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ActivityName result)
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
