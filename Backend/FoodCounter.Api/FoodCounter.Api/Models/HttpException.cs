using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FoodCounter.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class HttpException
    {
        public IEnumerable<Errors> Errors { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Errors
    {
        public string Message { get; set; }
    }
}
