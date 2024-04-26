using Aspire.Hosting;
using Concrete.Modeler.Extension.Registration;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var db = builder
	.AddSqlServer("database", password: builder.AddParameter("db-password", true))
	.WithDataVolume()
	.AddDatabase("Concrete")
	;

var modelerUi = builder.AddProject<Projects.Concrete_Web>("webfrontend");

var modeler = builder
	.AddProject<Projects.Concrete_Modeler>("concrete-modeler")
	.WithReference(db);

var quiz = BuildQuizesExtension(builder, modelerUi.GetEndpoint("https"));

modeler
	.AddModelerExtensions([quiz])
	.WithEnvironment("Logging__LogLevel__Default", "Debug");

modelerUi
	.WithReference(cache)
	.WithReference(modeler)
	.WithEnvironment("ModelerClient__ModelerUri", "https://concrete-modeler")
	.WithEnvironment("Logging__LogLevel__Default", "Debug")
;
builder.Build().Run();


static IResourceBuilder<ProjectResource> BuildQuizesExtension(IDistributedApplicationBuilder builder, EndpointReference modelerUri)
{
	var quizesUi = builder.AddProject<Projects.ConcreteExtensions_Quizes_UI>("concreteextensions-quizes-ui")
		.WithEnvironment("CrossOrigin__AllowedUrls__0", modelerUri)
		.WithEnvironment("Logging__LogLevel__Default", "Debug")
	;
	var quiz = builder.AddProject<Projects.Concrete_Extensions_Quizes_Api>("concrete-extensions-quizes-api")
		.WithReference(quizesUi)
		.WithEnvironment("Quiz__ActivityEditorUri", quizesUi.GetEndpoint("https"))
		;
	return quiz;
}
