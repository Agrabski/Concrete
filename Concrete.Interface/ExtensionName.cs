using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Concrete.Interface;

[JsonConverter(typeof(ParsableJsonConverter<ExtensionName>))]
public struct ExtensionName(params string[] parts) : IParsable<ExtensionName>, IEquatable<ExtensionName>
{
	public readonly string[] Parts { get; } = parts;
	public const char NamePartsSeparator = '.';

	public static ExtensionName Parse(string s, IFormatProvider? provider) => new(s.Split(NamePartsSeparator));
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ExtensionName result)
	{
		result = default;
		if (s is null)
			return false;
		var parts = s.Split(NamePartsSeparator);
		if (parts.Length > 1)
		{
			result = new(parts);
			return true;
		}
		return false;
	}
	public override readonly string ToString() => string.Join(NamePartsSeparator, Parts ?? []);
	public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is ExtensionName name && Equals(name);
	public readonly bool Equals(ExtensionName other)
	{
		if (other.Parts.Length != Parts.Length)
			return false;
		return Parts.Zip(other.Parts).All(a => a.First == a.Second);
	}

	public static bool operator ==(ExtensionName left, ExtensionName right)
	{
		return left.Equals(right);
	}

	public static bool operator !=(ExtensionName left, ExtensionName right)
	{
		return !(left == right);
	}

	public override readonly int GetHashCode()
	{
		return ToString().GetHashCode();
	}
}
