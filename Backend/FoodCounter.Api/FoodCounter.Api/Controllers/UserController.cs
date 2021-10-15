using AutoMapper;
using FoodCounter.Api.Entities;
using FoodCounter.Api.Models.Dto;
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
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUserCreationDto.Password);

            var result = await _userService.CreateAsync(newUser);

            var resultDto = _mapper.Map<UserFullDto>(result);

            return Ok(resultDto);
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

            object resultDto;

            if (Helpers.IdentityHelper.IsUserAdmin(User))
                resultDto = _mapper.Map<IEnumerable<UserFullDto>>(result);
            else
                resultDto = _mapper.Map<IEnumerable<UserLimitedDto>>(result);

            return Ok(resultDto);
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

            object resultDto;

            if (Helpers.IdentityHelper.IsUserAdmin(User))
                resultDto = _mapper.Map<UserFullDto>(result);
            else
                resultDto = _mapper.Map<UserLimitedDto>(result);

            return Ok(resultDto);
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="updatePassword">Model dor update password</param>
        /// <returns>Statuts that inform if password is updated</returns>
        [HttpPut("password")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserPassword(UserUpdatePasswordDto updatePassword)
        {
            var userId = Convert.ToInt64(User.Identity.Name);

            var user = await _userService.GetOneByIdAsync(userId);

            var newPasswordHashed = BCrypt.Net.BCrypt.HashPassword(updatePassword.Password);

            await _userService.UpdateUserPassword(user, newPasswordHashed);

            return NoContent();
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
        }

        #endregion
    }
}
