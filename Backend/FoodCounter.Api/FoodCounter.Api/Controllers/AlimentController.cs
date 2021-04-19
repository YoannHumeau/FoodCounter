using FoodCounter.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using FoodCounter.Api.Service;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _alimentService.GetAllAsync();

            return Ok(result);
        }
    }
}
