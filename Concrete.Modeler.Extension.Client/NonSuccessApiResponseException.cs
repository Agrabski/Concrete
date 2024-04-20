using System.Net;

namespace Concrete.Modeler.Extension.Client;

public class NonSuccessApiResponseException(HttpStatusCode code) : Exception($"Api call resulted in {code} status code")
{
}

