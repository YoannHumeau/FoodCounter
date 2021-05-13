using AutoMapper;
using FoodCounter.Api.Entities;
using FoodCounter.Api.Models;
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
    /// Users Controller
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="userService"></param>
        public UserController(ILogger<UserController> logger, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        #region User 

        /// <summary>
        /// Create a user
        /// </summary>
        /// <returns>User created</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync(UserCreationDto newUserCreationDto)
        {
            var newUser = _mapper.Map<User>(newUserCreationDto);

            // Check errors and send http code 

            var result = await _userService.CreateAsync(newUser);

            return Ok(result);
        }

        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns>All users</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _userService.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Get one user by id
        /// </summary>
        /// <returns>One user</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneByIdAsync([FromRoute] long id)
        {
            var result = await _userService.GetOneByIdAsync(id);

            if (result == null)
                return NotFound(new { Message = ResourceEn.UserNotFound });

            return Ok(result);
        }

        #endregion

        #region Authentication

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="userLoginDto">User login dto</param>
        /// <returns>User logged</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _userService.Authenticate(userLoginDto.Username, userLoginDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var userLoggedDto = _mapper.Map<UserLoggedDto>(user);

            return Ok(userLoggedDto);
        }

        /// <summary>
        /// Get the user logged informations
        /// </summary>
        /// <returns>User logged informations</returns>
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = Convert.ToInt64(User.Identity.Name);

            var user = await _userService.GetOneByIdAsync(userId);

            var userDto = _mapper.Map<UserFullDto>(user);

            return Ok(userDto);

            //TODO : Test me !
        }

        #endregion
    }
}
