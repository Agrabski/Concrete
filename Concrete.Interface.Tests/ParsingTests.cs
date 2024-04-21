using System.Text.Json;

namespace Concrete.Interface.Tests;

public class ParsingTests
{
	[Fact]
	public void ExtensionNameCanBeParsed()
	{
		var name = new ExtensionName("Concrete", "Core", "Quiz");
		var text = name.ToString();

		Assert.Equal(name, ExtensionName.Parse(text, null));
	}

	[Fact]
	public void ActivityNameNameCanBeParsed()
	{
		var name = new ActivityName(new("Concrete", "Core", "Quiz"), "test");
		var text = name.ToString();

		Assert.Equal(name, ActivityName.Parse(text, null));
	}

	[Fact]
	public void ActivityMetadataCollectionCanBeDeserialised()
	{
		var data = JsonSerializer.Deserialize<ActivityMetadata[]>("[{\"Name\":\"Concrete.Core.Quizes::Quiz\"}]");

		Assert.NotNull(data);
		var e = Assert.Single(data);
		Assert.Equal(new ActivityMetadata(new(new("Concrete", "Core", "Quizes"), "Quiz")), e);
	}
}
