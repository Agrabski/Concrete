using Aspire.Hosting;
using Concrete.Modeler.Extension.Registration;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var db = builder
	.AddSqlServer("database", password: builder.AddParameter("db-password", true))
	.WithDataVolume()
	.AddDatabase("Concrete")
	;

var data = builder.AddProject<Projects.Concrete_Core_Data_Api>("data")
	.WithReference(db)
	;

var modelerUi = builder.AddProject<Projects.Concrete_Web>("webfrontend")
	;


var modeler = builder
	.AddProject<Projects.Concrete_Modeler>("concrete-modeler")
	.WithReference(db);

var quiz = BuildQuizesExtension(builder, modelerUi.GetEndpoint("https"), data.GetEndpoint("https"));

modeler
	.AddModelerExtensions([quiz])
	.WithEnvironment("Logging__LogLevel__Default", "Debug");

modelerUi
	.WithReference(cache)
	.WithReference(modeler)
	.WithReference(data)
	.WithEnvironment("ModelerClient__ModelerUri", "https://concrete-modeler")
	.WithEnvironment("DataClient__DataApiUri", "https://data")
	.WithEnvironment("Logging__LogLevel__Default", "Debug")
;

builder.Build().Run();


IResourceBuilder<ProjectResource> BuildQuizesExtension(IDistributedApplicationBuilder builder, EndpointReference modelerUri, EndpointReference dataUri)
{
	var coreQuestions = builder.AddProject<Projects.Concrete_Extensions_Quizes_Questions_Core>("concreteextensions-quizes-questions")
		.WithReference(dataUri)
		.WithEnvironment("DataClient__DataApiUri", "https://data")
		;

	var quizesUi = builder.AddProject<Projects.ConcreteExtensions_Quizes_UI>("concreteextensions-quizes-ui")
		.WithReference(data)
		.WithReference(coreQuestions)
		.WithEnvironment("CrossOrigin__AllowedUrls__0", modelerUri)
		.WithEnvironment("Logging__LogLevel__Default", "Debug")
		.WithEnvironment("Data__DataApiUri", "https://data")
		.WithEnvironment("QuestionsClient__ExtensionUris__0", "https://concreteextensions-quizes-questions")
	;
	coreQuestions
		.WithEnvironment("CrossOrigin__AllowedUrls__0", quizesUi.GetEndpoint("https"))
		.WithEnvironment("CrossOrigin__AllowedUrls__1", modelerUri)
		;
	var quiz = builder.AddProject<Projects.Concrete_Extensions_Quizes_Api>("concrete-extensions-quizes-api")
		.WithReference(quizesUi)
		.WithEnvironment("Quiz__ActivityEditorUri", quizesUi.GetEndpoint("https"))
		;
	return quiz;
}

