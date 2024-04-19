using System.Diagnostics.CodeAnalysis;

namespace Concrete.Interface;

public record struct ExtensionName(params string[] Parts) : IParsable<ExtensionName>
{
	public static ExtensionName Parse(string s, IFormatProvider? provider) => new(s.Split('.'));
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ExtensionName result)
	{
		result = default;
		if (s is null)
			return false;
		var parts = s.Split('.');
		if (parts.Length > 1)
		{
			result = new(parts);
			return true;
		}
		return false;
	}
	public override readonly string ToString() => string.Join('.', Parts);
}
