using FoodCounter.Api.Entities;
using FoodCounter.Api.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service
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
            // TODO : Check that user already exists
            if (_userRepository.GetOneByEmailAsync(newUser.Email) != null)
                throw new ArgumentException(Resources.ResourceEn.EmailAlreadyExists);

            if (_userRepository.GetOneByUsernameAsync(newUser.Username) != null)
                throw new ArgumentException(Resources.ResourceEn.UsernameAlreadyExists);

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

            return result;
        }

        #endregion

        #region Authentication

        ///<inheritdoc/>
        public async Task<User> Authenticate(string username, string password)
        {
            //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            var user = await _userRepository.GetOneByUsernameAndPassword(username, password);

            // return null if user not found
            if (user == null)
                return null;

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
