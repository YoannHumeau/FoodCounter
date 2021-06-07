using System.Net;

namespace FoodCounter.Api.Exceptions
{
    public class HttpNotFoundException : HttpStatusException
    {
        public HttpNotFoundException(string msg) : base(msg)
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
