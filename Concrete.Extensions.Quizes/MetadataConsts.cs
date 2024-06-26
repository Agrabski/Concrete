﻿using Concrete.Interface;

namespace Concrete.Extensions.Quizes.Api;

public static class MetadataConsts
{
	public static ExtensionName ExtensionName() => new("Concrete", "Core", "Quizes");
	public static ActivityTypeName QuizActivityName() => new(ExtensionName(), "Quiz");
}
