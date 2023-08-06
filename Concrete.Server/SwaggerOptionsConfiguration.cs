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
		s.SupportNonNullableReferenceTypes();
		s.SchemaFilter<RequiredNotNullableSchemaFilter>();
		s.UseAllOfToExtendReferenceSchemas();
		s.UseAllOfForInheritance();
		s.UseOneOfForPolymorphism();
		s.SelectSubTypesUsing(baseType =>
		{
			var info = _infoResolver.GetTypeInfo(baseType, new());
			if (info.PolymorphismOptions is not null)
				return info.PolymorphismOptions.DerivedTypes.Select(d => d.DerivedType);
			return Enumerable.Empty<Type>();
		});
	}
}
