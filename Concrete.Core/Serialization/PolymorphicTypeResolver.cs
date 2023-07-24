using Concrete.Core.Activities.Instances;
using Concrete.Core.Activities.Templates;
using Concrete.Core.Questions.Instances;
using Concrete.Core.Questions.Templates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Concrete.Core.Serialization;

internal class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
{
	private readonly PolymorphicTypeInfo<IQuestion>[] _questionInfos;
	private readonly PolymorphicTypeInfo<IQuestionTemplate>[] _templateInfos;
	private readonly PolymorphicTypeInfo<IQuestionAnswer>[] _answerInfos;
	private readonly PolymorphicTypeInfo<IActivity>[] _activityInfos;
	private readonly PolymorphicTypeInfo<IActivityTemplate>[] _activityTemplateInfos;
	public PolymorphicTypeResolver(
		IEnumerable<PolymorphicTypeInfo<IQuestion>> questionInfos,
		IEnumerable<PolymorphicTypeInfo<IQuestionTemplate>> templateInfos,
		IEnumerable<PolymorphicTypeInfo<IQuestionAnswer>> answerInfos,
		IEnumerable<PolymorphicTypeInfo<IActivity>> activityInfos,
		IEnumerable<PolymorphicTypeInfo<IActivityTemplate>> activityTemplateInfos
		)
	{
		_questionInfos = questionInfos.ToArray();
		_templateInfos = templateInfos.ToArray();
		_answerInfos = answerInfos.ToArray();
		_activityInfos = activityInfos.ToArray();
		_activityTemplateInfos = activityTemplateInfos.ToArray();
	}
	public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
	{
		var jsonTypeInfo = base.GetTypeInfo(type, options);
		if (type == typeof(IQuestion) || type.GetInterfaces().Any(i =>
				i.IsConstructedGenericType &&
				i.GetGenericTypeDefinition() == typeof(IQuestion<>)
		))
			return GetOptionsFor(jsonTypeInfo, _questionInfos);
		if (type == typeof(IQuestionTemplate) || type.GetInterfaces().Any(i =>
				i.IsConstructedGenericType &&
				i.GetGenericTypeDefinition() == typeof(IQuestionTemplate<>)
		))
			return GetOptionsFor(jsonTypeInfo, _templateInfos);

		if (type == typeof(IQuestionAnswer))
			return GetOptionsFor(jsonTypeInfo, _answerInfos);


		if (type == typeof(IActivity))
			return GetOptionsFor(jsonTypeInfo, _activityInfos);
		if (type == typeof(IActivityTemplate))
			return GetOptionsFor(jsonTypeInfo, _activityTemplateInfos);

		return base.GetTypeInfo(type, options);
	}
	private JsonTypeInfo GetOptionsFor<T>(JsonTypeInfo baseInfo, IEnumerable<PolymorphicTypeInfo<T>> typeInfos)
	{
		baseInfo.PolymorphismOptions = new JsonPolymorphismOptions
		{
			TypeDiscriminatorPropertyName = "$$type",
			IgnoreUnrecognizedTypeDiscriminators = true,
			UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
		};
		foreach (var t in typeInfos)
			baseInfo.PolymorphismOptions.DerivedTypes.Add(t.TypeInfo);
		return baseInfo;
	}
}
