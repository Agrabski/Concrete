﻿using Concrete.Serialization;
using System.Text.Json;

namespace Concrete.Extensions.Quizes.Questions.Template;
public sealed class QuestionTemplate : IPolymorphicDataHolder<QuestionTypeName>
{
	public required string Name { get; set; }
	public QuestionTypeName Discriminator { get; set; }
	public QuestionTag[] Tags { get; set; } = [];
	public QuestionCategory? Category { get; set; }
	public required JsonDocument Data { get; set; }
}

