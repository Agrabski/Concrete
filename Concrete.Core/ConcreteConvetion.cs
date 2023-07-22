namespace Concrete.Core;

public static class ConcreteConvetion
{
	public static string TypeDiscriminator(string name, params string[] namespaceParts) => string.Join("::", namespaceParts.Append(name));
}
