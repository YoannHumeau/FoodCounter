using System.Net;

namespace FoodCounter.Api.Exceptions
{
    public class HttpConflictException : HttpStatusException
    {
        public HttpConflictException(string msg) : base(msg)
        {
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
