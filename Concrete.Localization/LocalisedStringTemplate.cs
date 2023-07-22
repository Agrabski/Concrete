using SmartFormat;

namespace Concrete.Localization;
public class LocalisedStringTemplate
{
	private readonly Dictionary<string, string> _templatesByLocale = new();

	public LocalisedStringTemplate()
	{
	}

	public LocalisedStringTemplate(Dictionary<string, string> templatesByLocale)
	{
		_templatesByLocale = templatesByLocale;
	}

	public string FillTemplateForLocale(string locale, Dictionary<string, object> parameters)
	{
		// todo: verify locale
		return Smart.Format(_templatesByLocale[locale], parameters);
	}

	public LocalisedString Fill(Dictionary<string, object> parameters)
	{
		return new(_templatesByLocale.ToDictionary(kv => kv.Key, kv => Smart.Format(kv.Value, parameters)));
	}
}
