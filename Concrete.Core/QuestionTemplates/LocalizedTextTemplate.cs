namespace Concrete.Core.QuestionTemplates;

public class LocalizedTextTemplate
{
	public Dictionary<string, string> TextByLocaleId { get; init; } = new();
	public string FillForLocale(string localeId, object[] parameters)
	{
		return string.Format(TextByLocaleId[localeId], parameters);
	}
}
