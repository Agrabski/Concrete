namespace Concrete.Localization;

public class LocalisedString
{
	private readonly Dictionary<string, string> _textByLocale = new();

	public LocalisedString(Dictionary<string, string> textByLocale)
	{
		_textByLocale = textByLocale;
	}

	public string GetTextForLocale(string locale)
	{
		// todo: verify locale
		return _textByLocale[locale];
	}
}
