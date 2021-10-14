using System.Net;

namespace FoodCounter.Api.Exceptions
{
    public class HttpInternalErrorException : HttpStatusException
    {
        public HttpInternalErrorException(string msg) : base(msg)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}
