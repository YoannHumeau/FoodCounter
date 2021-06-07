using System;
using System.Net;

namespace FoodCounter.Api.Exceptions
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode StatusCode { get; protected set; }

        public HttpStatusException(string msg) : base(msg)
        {

        }
    }
}
