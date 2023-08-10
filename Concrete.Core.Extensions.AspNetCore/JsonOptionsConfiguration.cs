using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Concrete.Core.Extensions.AspNetCore;

internal class JsonOptionsConfiguration : IConfigureOptions<JsonOptions>
{
	private readonly IJsonTypeInfoResolver _resolver;

	public JsonOptionsConfiguration(IJsonTypeInfoResolver resolver)
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
