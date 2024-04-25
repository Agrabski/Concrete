using Aspire.Hosting;
using Concrete.Modeler.Extension.Registration;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var db = builder
	.AddSqlServer("database", password: builder.AddParameter("db-password", true))
	.WithDataVolume()
	.AddDatabase("Concrete")
	;

var quiz = BuildQuizesExtension(builder);

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


static IResourceBuilder<ProjectResource> BuildQuizesExtension(IDistributedApplicationBuilder builder)
{
	var quizesUi = builder.AddProject<Projects.ConcreteExtensions_Quizes_UI>("concreteextensions-quizes-ui");
	var quiz = builder.AddProject<Projects.Concrete_Extensions_Quizes_Api>("concrete-extensions-quizes-api")
		.WithReference(quizesUi)
		.WithEnvironment("Quiz__ActivityEditorUri", "https://concreteextensions-quizes-ui")
		;
	return quiz;
}
