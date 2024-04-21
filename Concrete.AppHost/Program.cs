using Concrete.Modeler.Extension.Registration;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var db = builder.AddSqlServer("database").AddDatabase("Concrete");

builder.AddProject<Projects.Concrete_Web>("webfrontend")
	.WithReference(cache)
;

var quiz = builder.AddProject<Projects.Concrete_Extensions_Quizes_Api>("concrete-extensions-quizes-api");
builder
	.AddProject<Projects.Concrete_Modeler>("concrete-modeler")
	.WithReference(db)
	.AddModelerExtensions([quiz])
	.WithEnvironment("Logging__LogLevel__Default", "Debug");

builder.Build().Run();
