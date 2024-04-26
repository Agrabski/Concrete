namespace Concrete.CrossOriginFrameConfiguration;

public class CrossOriginConfig
{
	public List<Uri> AllowedUrls { get; set; } = [];
	public List<string> AdditionalCspDirectives { get; set; } = [];
}
