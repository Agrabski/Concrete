using Concrete.Modeler.Extension;
using Concrete.Modeler.Extension.Registration;
var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

builder.AddProject<Projects.Concrete_Web>("webfrontend")
	.WithReference(cache)
;

var quiz = builder.AddProject("concrete-extensions-quizes-api", "../Concrete.Extensions.Quizes.Api/Concrete.Extensions.Quizes.Api.csproj");
builder.AddProject("concrete-modeler", "../Concrete.Modeler/Concrete.Modeler.csproj")
	.AddModelerExtensions(new()
	{
		[new("Concrete", "Core", "Quizes")] = quiz
	}
);

builder.Build().Run();
