﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodCounter.Api.Service;
using System.Threading.Tasks;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Models.Dto;
using FoodCounter.Api.Models;
using AutoMapper;

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
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="alimentService"></param>
        public AlimentController(ILogger<AlimentController> logger, IAlimentService alimentService,
            IMapper mapper)
        {
            _logger = logger;
            _alimentService = alimentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create aliment
        /// </summary>
        /// <returns>List of aliments</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(AlimentCreationModelDto newAlimentDto)
        {
            var newAliment = _mapper.Map<AlimentModel>(newAlimentDto);

            var result = await _alimentService.CreateAsync(newAliment);

            return Ok(result);
        }

        /// <summary>
        /// Get all the aliments or one aliment by name
        /// </summary>
        /// <returns>List of aliments</returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string name)
        {
            if (name != null)
            {
                var resultByName = await _alimentService.GetOneByNameAsync(name);

                if (resultByName == null)
                    return NotFound(new { Message = ResourceEn.AlimentNotFound });

                return Ok(resultByName);
            }

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
                return NotFound(new { Message = ResourceEn.AlimentNotFound });

            return Ok(result);
        }
    }
}
