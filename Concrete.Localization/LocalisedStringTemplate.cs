using SmartFormat;

namespace Concrete.Localization;
public class LocalisedStringTemplate
{
	public Dictionary<string, string> TemplatesByLocale { get; init; } = new();
	public LocalisedStringTemplate()
	{
	}
	public LocalisedStringTemplate(Dictionary<string, string> templatesByLocale)
	{
		TemplatesByLocale = templatesByLocale;
	}

	public string FillTemplateForLocale(string locale, Dictionary<string, object> parameters)
	{
		// todo: verify locale
		return Smart.Format(TemplatesByLocale[locale], parameters);
	}

	public LocalisedString Fill(Dictionary<string, object> parameters)
	{
		return new(TemplatesByLocale.ToDictionary(kv => kv.Key, kv => Smart.Format(kv.Value, parameters)));
	}
}
