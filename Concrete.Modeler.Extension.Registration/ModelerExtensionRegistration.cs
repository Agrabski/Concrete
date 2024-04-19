using Concrete.Interface;

namespace Concrete.Modeler.Extension.Registration;

public static class ModelerExtensionRegistration
{
	public static IResourceBuilder<ProjectResource> AddModelerExtensions(
		this IResourceBuilder<ProjectResource> builder, 
		Dictionary<ExtensionName, IResourceBuilder<IResourceWithServiceDiscovery>> names)
	{
		foreach (var (name, resource) in names)
		{
			builder.WithEnvironment($"Extensions__{name}", $"https+http://{resource.Resource.Name}");
			builder.WithReference(resource);
		}
		return builder;
	}
}
