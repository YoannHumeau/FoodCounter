using FoodCounter.Api.Entities;
using FoodCounter.Api.Repositories;
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
        public async Task<User> GetOneByIdAsync(long id)
        {
            var result = await _userRepository.GetOneByIdAsync(id);

            return result;
        }
    }
}
