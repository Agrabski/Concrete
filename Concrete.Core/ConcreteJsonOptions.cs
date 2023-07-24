﻿using Concrete.Core.Serialization;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Concrete.Core;

internal class ConcreteJsonOptions : IConfigureOptions<JsonSerializerOptions>
{
	private readonly PolymorphicTypeResolver _resolver;

	public ConcreteJsonOptions(PolymorphicTypeResolver resolver)
	{
		_resolver = resolver;
	}

	public void Configure(JsonSerializerOptions options)
	{
		options.TypeInfoResolver = _resolver;
		options.WriteIndented = true;
		options.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;
	}
}
