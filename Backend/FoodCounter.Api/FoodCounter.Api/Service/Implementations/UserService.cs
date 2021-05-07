using FoodCounter.Api.Entities;
using FoodCounter.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service
{
    /// <summary>
    /// User service class
    /// </summary>
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        ///<inheritdoc/>
        public async Task<User> CreateAsync(User newUser)
        {
            // TODO : Check that user already exists

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
    }
}
