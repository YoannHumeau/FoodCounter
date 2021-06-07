using System.Collections.Generic;

namespace FoodCounter.Api.Models
{
    public class HttpException
    {
        public IEnumerable<Errors> Errors { get; set; }
    }

    public class Errors
    {
        public string Message { get; set; }
    }
}
