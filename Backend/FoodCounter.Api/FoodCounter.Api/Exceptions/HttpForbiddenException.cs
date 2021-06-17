using System.Net;

namespace FoodCounter.Api.Exceptions
{
    public class HttpForbiddenException : HttpStatusException
    {
        public HttpForbiddenException(string msg) : base(msg)
        {
            StatusCode = HttpStatusCode.Forbidden;
        }
    }
}
