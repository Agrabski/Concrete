using Concrete.Core.Activities.Instances;
using Concrete.Core.Activities.Templates;
using Concrete.Core.Questions.Instances;
using Concrete.Core.Questions.Templates;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Concrete.Core.Serialization;
internal class ConcreteSerializer : IConcreteSerializer
{
	private readonly PolymorphicTypeInfo<IQuestion>[] _questionInfos;
	private readonly PolymorphicTypeInfo<IQuestionTemplate>[] _templateInfos;
	private readonly PolymorphicTypeInfo<IQuestionAnswer>[] _answerTemplates;
	private readonly PolymorphicTypeInfo<IActivity>[] _activityInfos;
	private readonly PolymorphicTypeInfo<IActivityTemplate>[] _activityTemplateInfos;
	private JsonSerializerOptions? _jsonSerializerOptions;
	private JsonSerializerOptions Options => _jsonSerializerOptions ??= GetOptions();

	private JsonSerializerOptions GetOptions()
	{
		return new JsonSerializerOptions()
		{
			TypeInfoResolver = new PolymorphicTypeResolver(
				_questionInfos,
				_templateInfos,
				_answerTemplates,
				_activityInfos,
				_activityTemplateInfos
			),
			WriteIndented = true,
			UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement
		};
	}

	public ConcreteSerializer(
		IEnumerable<PolymorphicTypeInfo<IQuestion>> questionInfos,
		IEnumerable<PolymorphicTypeInfo<IQuestionTemplate>> templateInfos,
		IEnumerable<PolymorphicTypeInfo<IQuestionAnswer>> answerTemplates,
		IEnumerable<PolymorphicTypeInfo<IActivity>> activityInfos,
		IEnumerable<PolymorphicTypeInfo<IActivityTemplate>> activityTemplateInfos)
	{
		_questionInfos = questionInfos.ToArray();
		_templateInfos = templateInfos.ToArray();
		_answerTemplates = answerTemplates.ToArray();
		_activityInfos = activityInfos.ToArray();
		_activityTemplateInfos = activityTemplateInfos.ToArray();
	}

	public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, Options);
	public T Deserialize<T>([StringSyntax(StringSyntaxAttribute.Json)] string json) => JsonSerializer.Deserialize<T>(json, Options) ?? throw new Exception();
}
