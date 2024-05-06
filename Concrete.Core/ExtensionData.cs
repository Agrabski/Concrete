using Concrete.Interface;
using System.Text.Json;

namespace Concrete.Core;

public class ExtensionData
{
	public required string Key { get; set; }
	public required string Category { get; set; }
	public ExtensionName ExtensionName { get; set; }
	public required DateTime Modified { get; set; }
	public required JsonDocument Value { get; set; }
}
