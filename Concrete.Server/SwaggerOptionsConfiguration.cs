using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization.Metadata;

namespace Concrete.Server;

// todo: move to a package
internal class SwaggerOptionsConfiguration : IConfigureOptions<SwaggerGenOptions>
{
	private readonly DefaultJsonTypeInfoResolver _infoResolver;

	public SwaggerOptionsConfiguration(DefaultJsonTypeInfoResolver infoResolver)
	{
		_infoResolver = infoResolver;
	}

	public void Configure(SwaggerGenOptions s)
	{
		var typeDiscriminators = new Dictionary<Type, string>();
		s.SupportNonNullableReferenceTypes();
		s.SchemaFilter<RequiredNotNullableSchemaFilter>();
		s.UseAllOfForInheritance();
		s.SelectSubTypesUsing(baseType =>
		{
			var info = _infoResolver.GetTypeInfo(baseType, new());
			if (info.PolymorphismOptions is not null)
			{
				foreach (var derivedType in info.PolymorphismOptions.DerivedTypes)
					typeDiscriminators[derivedType.DerivedType] = derivedType.TypeDiscriminator as string ?? string.Empty;
				return info.PolymorphismOptions.DerivedTypes.Select(d => d.DerivedType);
			}
			return Enumerable.Empty<Type>();
		});
		s.SelectDiscriminatorNameUsing(baseType =>
		{
			var info = _infoResolver.GetTypeInfo(baseType, new());
			if (info.PolymorphismOptions is not null)
				return info.PolymorphismOptions.TypeDiscriminatorPropertyName;
			return string.Empty;

		});
		s.SelectDiscriminatorValueUsing(type =>
		{
			if (typeDiscriminators.ContainsKey(type))
				return typeDiscriminators[type];
			return string.Empty;
		});
	}
}
