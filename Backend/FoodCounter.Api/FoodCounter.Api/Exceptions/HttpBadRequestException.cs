using System.Net;

namespace FoodCounter.Api.Exceptions
{
    public class HttpBadRequestException : HttpStatusException
    {
        public HttpBadRequestException(string msg) : base(msg)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
