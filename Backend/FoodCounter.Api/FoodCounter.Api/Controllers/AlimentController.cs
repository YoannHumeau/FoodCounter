using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodCounter.Api.Service;
using System.Threading.Tasks;
using FoodCounter.Api.Resources;
using Newtonsoft.Json;

namespace FoodCounter.Api.Controllers
{
    /// <summary>
    /// Aliments Controller
    /// </summary>
    [ApiController]
    [Route("aliments")]
    public class AlimentController : ControllerBase
    {
        private readonly ILogger<AlimentController> _logger;
        private readonly IAlimentService _alimentService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="alimentService"></param>
        public AlimentController(ILogger<AlimentController> logger,
            IAlimentService alimentService)
        {
            _logger = logger;
            _alimentService = alimentService;
        }

        /// <summary>
        /// Get all the aliments
        /// </summary>
        /// <returns>List of aliments</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _alimentService.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Get one aliment by id
        /// </summary>
        /// <returns>One aliment</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneByIdAsync([FromRoute] long id)
        {
            var result = await _alimentService.GetOneByIdAsync(id);

            if (result == null)
                return NotFound(JsonConvert.SerializeObject(new { Message = ResourceEn.AlimentNotFound }));

            return Ok(result);
        }
    }
}
