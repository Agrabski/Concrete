namespace Concrete.Localization;

public class LocalisedString
{
	public Dictionary<string, string> TextByLocale { get; init; } = new();
	public LocalisedString() { }
	public LocalisedString(Dictionary<string, string> textByLocale)
	{
		TextByLocale = textByLocale;
	}


	public string GetTextForLocale(string locale)
	{
		// todo: verify locale
		return TextByLocale[locale];
	}
}
