using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodCounter.Api.Services;
using System.Threading.Tasks;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Models.Dto;
using FoodCounter.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using FoodCounter.Api.Entities;
using System.Collections.Generic;

namespace FoodCounter.Api.Controllers
{
    /// <summary>
    /// Aliments Controller
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("aliments")]
    public class AlimentController : ControllerBase
    {
        private readonly ILogger<AlimentController> _logger;
        private readonly IAlimentService _alimentService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="alimentService"></param>
        public AlimentController(ILogger<AlimentController> logger, IMapper mapper, IAlimentService alimentService)
        {
            _logger = logger;
            _alimentService = alimentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create an aliment
        /// </summary>
        /// <returns>Aliment created</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> CreateAsync(AlimentCreationDto newAlimentDto)
        {
            var newAliment = _mapper.Map<Aliment>(newAlimentDto);

            var result = await _alimentService.CreateAsync(newAliment);

            return Ok(result);
        }

        /// <summary>
        /// Get all the aliments or one aliment by name in query ?name=
        /// </summary>
        /// <param name="name">name of thaliment</param>
        /// <returns>All aliments or one by search name</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync([FromQuery] string name)
        {
            if (name != null)
            {
                var resultOneByName = await _alimentService.GetOneByNameAsync(name);

                var resultOneByNameDto = _mapper.Map<AlimentDto>(resultOneByName);

                return Ok(resultOneByNameDto);
            }

            var resultList = await _alimentService.GetAllAsync();

            var resultListDto = _mapper.Map<IEnumerable<AlimentDto>>(resultList);

            return Ok(resultListDto);
        }

        /// <summary>
        /// Get one aliment by id
        /// </summary>
        /// <returns>One aliment</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneByIdAsync([FromRoute] long id)
        {
            var result = await _alimentService.GetOneByIdAsync(id);

            var resultDto = _mapper.Map<AlimentDto>(result);

            return Ok(resultDto);
        }

        /// <summary>
        /// Update an aliment
        /// </summary>
        /// <returns>Aliment updated</returns>
        [HttpPut("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, AlimentUpdateDto updateAlimentDto)
        {
            var updateAliment = _mapper.Map<Aliment>(updateAlimentDto);
            updateAliment.Id = id;

            var result = await _alimentService.UpdateAsync(updateAliment);

            var resultDto = _mapper.Map<AlimentDto>(result);

            return Ok(resultDto);
        }

        /// <summary>
        /// Delete an aliment
        /// </summary>
        /// <returns>Boolean</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            await _alimentService.DeleteAsync(id);
            
            return NoContent();
        }
    }
}
