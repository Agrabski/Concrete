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
	public PolymorphicTypeResolver(
		IEnumerable<PolymorphicTypeInfo<IQuestion>> questionInfos,
		IEnumerable<PolymorphicTypeInfo<IQuestionTemplate>> templateInfos
		)
	{
		_questionInfos = questionInfos.ToArray();
		_templateInfos = templateInfos.ToArray();
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
