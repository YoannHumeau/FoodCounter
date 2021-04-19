using FoodCounter.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using FoodCounter.Api.Service;

namespace FoodCounter.Api.Controllers
{
    [ApiController]
    [Route("aliments")]
    public class AlimentController : ControllerBase
    {
        private readonly ILogger<AlimentController> _logger;
        private readonly IAlimentService _alimentService;

        public AlimentController(ILogger<AlimentController> logger,
            IAlimentService alimentService)
        {
            _logger = logger;
            _alimentService = alimentService;
        }

        [HttpGet]
        public IEnumerable<AlimentModel> GetAll()
        {
            var result =  _alimentService.GetAll();

            return result;
        }
    }
}
