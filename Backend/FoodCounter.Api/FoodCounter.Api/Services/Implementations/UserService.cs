using FoodCounter.Api.Entities;
using FoodCounter.Api.Exceptions;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoodCounter.Api.Services.Implementations
{
    /// <summary>
    /// User service class
    /// </summary>
    public class UserService : IUserService
    {
        private IConfiguration _configuration;
        private IUserRepository _userRepository;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userRepository"></param>
        public UserService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        #region User

        ///<inheritdoc/>
        public async Task<User> CreateAsync(User newUser)
        {
            if (await _userRepository.GetOneByEmailAsync(newUser.Email) != null)
                throw new HttpConflictException(Resources.ResourceEn.EmailAlreadyExists);

            if (await _userRepository.GetOneByUsernameAsync(newUser.Username) != null)
                throw new HttpConflictException(Resources.ResourceEn.UsernameAlreadyExists);

            var result = await _userRepository.CreateAsync(newUser);

            return result;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _userRepository.GetAllAsync();

            return result;
        }

        ///<inheritdoc/>
        public async Task<User> GetOneByIdAsync(long id)
        {
            var result = await _userRepository.GetOneByIdAsync(id);

            if (result == null)
                throw new HttpNotFoundException(ResourceEn.UserNotFound);

            return result;
        }

        ///<inheritdoc/>
        public async Task<bool> UpdateUserPassword(User user, string newPassword)
        {
            var result = await _userRepository.UpdateUserPassword(user, newPassword);

            if (!result)
                throw new HttpInternalErrorException(ResourceEn.PasswordNotUpdated);

            return result;
        }

        #endregion

        #region Authentication

        ///<inheritdoc/>
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetOneByUsernameAsync(username);

            // If user not found return BadRequest Exception
            if (user == null)
                throw new HttpBadRequestException(ResourceEn.UserBadAuthentication);

            // If bad password return BadRequest Exception
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new HttpBadRequestException(ResourceEn.UserBadAuthentication);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Authentication:SecretJwtKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        #endregion
    }
}
