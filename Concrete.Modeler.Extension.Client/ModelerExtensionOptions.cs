using Concrete.Interface;

namespace Concrete.Modeler.Extension.Client;

public class ModelerExtensionOptions
{
	public Dictionary<ExtensionName, Uri> ExtensionAddresses { get; set; } = [];
}
