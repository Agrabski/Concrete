using Concrete.Interface;

namespace Concrete.Core.Data.Api.Client;

public class DataClientConfiguration
{
	public Uri DataApiUri { get; set; } = new("http://localhost");
}
