using System.Diagnostics.CodeAnalysis;

namespace Concrete.Core.Activities.Templates;

public record struct CategoryName(params string[] Parts) : IParsable<CategoryName>
{
	public string Name => ToString();
	public static CategoryName Parse(string s, IFormatProvider? provider) => new(s.Split("."));
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out CategoryName result)
	{
		if (s == null)
		{
			result = default;
			return false;
		}
		result = Parse(s, provider);
		return true;
	}

	public override string ToString() => string.Join(".", Parts);

	public readonly bool Contains(CategoryName name)
	{
		if (name.Parts.Length <= Parts.Length)
		{
			foreach (var (a, b) in name.Parts.Zip(Parts))
				if (a != b)
					return false;
			return true;
		}
		return false;
	}
}
