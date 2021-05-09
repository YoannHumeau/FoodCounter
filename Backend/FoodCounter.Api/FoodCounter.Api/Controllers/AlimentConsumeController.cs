using AutoMapper;
using FoodCounter.Api.Models.Dto;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FoodCounter.Api.Controllers
{
    /// <summary>
    /// AlimentsConsume Controller
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("aliment-consumes")]
    public class AlimentConsumeController : ControllerBase
    {
        private readonly ILogger<AlimentConsumeController> _logger;
        private readonly IAlimentConsumeService _alimentConsumeService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="alimentConsumeService"></param>
        public AlimentConsumeController(ILogger<AlimentConsumeController> logger, IMapper mapper, IAlimentConsumeService alimentConsumeService)
        {
            _logger = logger;
            _alimentConsumeService = alimentConsumeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get one aliment consume by id
        /// </summary>
        /// <returns>One aliment consume</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneByIdAsync([FromRoute] long id)
        {
            var result = await _alimentConsumeService.GetOneByIdAsync(id);

            if (result == null)
                return NotFound(new { Message = ResourceEn.AlimentConsumeNotFound });

            // TODO : check with role admin passtrought
            if (result.UserId != Convert.ToInt64(User.Identity.Name))
                return Forbid();

            var resultDto = _mapper.Map<AlimentConsumeDto>(result);

            return Ok(resultDto);
        }
    }
}
