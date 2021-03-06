using AutoMapper;
using FoodCounter.Api.Models;
using FoodCounter.Api.Models.Dto;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        /// Create an aliment consume
        /// </summary>
        /// <returns>Aliment consume created</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(AlimentConsumeCreationDto newAlimentConsumeDto)
        {
            if (newAlimentConsumeDto.ConsumeDate == null)
                newAlimentConsumeDto.ConsumeDate = DateTime.UtcNow;

            newAlimentConsumeDto.ConsumeDate = DateTime.Parse(newAlimentConsumeDto.ConsumeDate.ToString());

            var newAliment = _mapper.Map<AlimentConsume>(newAlimentConsumeDto);
            newAliment.UserId = Convert.ToInt64(User.Identity.Name);

            var result = await _alimentConsumeService.CreateAsync(newAliment);

            var resultDto = _mapper.Map<AlimentConsumeDto>(result);

            return Ok(resultDto);
        }

        /// <summary>
        /// Get all the aliment consumes for a user (By default Himself) 
        /// </summary>
        /// <param name="userId">User Id to ask aliment consumes</param>
        /// <returns>All aliments or one by search name</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync([FromQuery] long userId)
        {
            userId = userId == 0 ? Convert.ToInt64(User.Identity.Name) : userId;

            var result = await _alimentConsumeService.GetAllByUserIdAsync(userId);

            var resultDto = _mapper.Map<IEnumerable<AlimentConsumeDto>>(result);

            return Ok(resultDto);
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

            var resultDto = _mapper.Map<AlimentConsumeDto>(result);

            return Ok(resultDto);
        }

        /// <summary>
        /// Update an aliment consume
        /// </summary>
        /// <returns>Aliment consume updated</returns>
        [HttpPut("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, AlimentConsumeUpdateDto updateAlimentDto)
        {
            var updateConsumeAliment = _mapper.Map<AlimentConsume>(updateAlimentDto);
            updateConsumeAliment.Id = id;

            var result = await _alimentConsumeService.UpdateAsync(updateConsumeAliment);
            var resultDto = _mapper.Map<AlimentConsumeDto>(result);

            return Ok(resultDto);
        }

        /// <summary>
        /// Delete an aliment consume
        /// </summary>
        /// <returns>Status code if deleted or not</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            var result = await _alimentConsumeService.DeleteAsync(id);

            if (result)
                return NoContent();
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ResourceEn.ProblemDeleting });
        }
    }
}
