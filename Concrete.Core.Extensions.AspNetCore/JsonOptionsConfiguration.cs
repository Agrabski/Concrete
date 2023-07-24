using Concrete.Core.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace Concrete.Core.Extensions.AspNetCore;

internal class JsonOptionsConfiguration : IConfigureOptions<JsonOptions>
{
	private readonly PolymorphicTypeResolver _resolver;

	public JsonOptionsConfiguration(PolymorphicTypeResolver resolver)
	{
		_resolver = resolver;
	}

	public void Configure(JsonOptions options)
	{
		options.JsonSerializerOptions.TypeInfoResolver = _resolver;
		options.JsonSerializerOptions.WriteIndented = true;
		options.JsonSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;

	}
}
