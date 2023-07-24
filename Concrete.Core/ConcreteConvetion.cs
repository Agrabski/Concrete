namespace Concrete.Core;

public static class ConcreteConvetion
{
	public static string TypeDiscriminator(params string[] namespaceParts) => string.Join("::", namespaceParts);
}
