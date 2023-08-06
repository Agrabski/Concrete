namespace Concrete.Server;

public class SwaggerHostFactory
{
	public static IHost CreateHost()
	{
		return HostHelper.BuildHost(Array.Empty<string>());
	}
}
