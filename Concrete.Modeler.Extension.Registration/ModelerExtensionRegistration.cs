namespace Concrete.Modeler.Extension.Registration;

public static class ModelerExtensionRegistration
{
	public static IResourceBuilder<ProjectResource> AddModelerExtensions(
		this IResourceBuilder<ProjectResource> builder,
		IEnumerable<IResourceBuilder<IResourceWithServiceDiscovery>> extensions)
	{
		foreach (var ( resource, count) in extensions.Select((e, i) => (e, i)))
		{
			builder.WithEnvironment($"Extensions__ExtensionAddresses__{count}", $"http://{resource.Resource.Name}");
			builder.WithReference(resource);
		}
		return builder;
	}
}
