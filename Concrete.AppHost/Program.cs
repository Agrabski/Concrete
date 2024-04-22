using Aspire.Hosting;
using Concrete.Modeler.Extension.Registration;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var db = builder.AddSqlServer("database").AddDatabase("Concrete");


var quiz = builder.AddProject<Projects.Concrete_Extensions_Quizes_Api>("concrete-extensions-quizes-api");
var modeler = builder
	.AddProject<Projects.Concrete_Modeler>("concrete-modeler")
	.WithReference(db)
	.AddModelerExtensions([quiz])
	.WithEnvironment("Logging__LogLevel__Default", "Debug");

builder.AddProject<Projects.Concrete_Web>("webfrontend")
	.WithReference(cache)
	.WithReference(modeler)
	.WithEnvironment("ModelerClient__ModelerUri", "https://concrete-modeler")
;
builder.Build().Run();
