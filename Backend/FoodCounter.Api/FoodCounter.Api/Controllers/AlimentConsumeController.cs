using AutoMapper;
using FoodCounter.Api.Models.Dto;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
//using System.Security.Claims;

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
            if (userId == 0)
            {
                userId = Convert.ToInt64(User.Identity.Name);
            }
            else if (userId != Convert.ToInt64(User.Identity.Name) && !Helpers.IdentityHelper.IsUserAdmin(User))
            {
                return Forbid();
            }

            var result = await _alimentConsumeService.GetAllByUserIdAsync(userId);

            var resultDto = _mapper.Map<IEnumerable<AlimentConsumeDto>>(result);

            return Ok(result);
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

            if (result.UserId != Convert.ToInt64(User.Identity.Name) && !Helpers.IdentityHelper.IsUserAdmin(User))
                return Forbid();

            var resultDto = _mapper.Map<AlimentConsumeDto>(result);

            return Ok(resultDto);
        }
    }
}
