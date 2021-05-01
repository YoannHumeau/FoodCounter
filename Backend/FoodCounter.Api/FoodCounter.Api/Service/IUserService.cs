using FoodCounter.Api.Entities;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service
{
    /// <summary>
    /// User service class
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <returns>Aliment created</returns>
        public Task<User> CreateAsync(User newUser);

        /// <summary>
        /// Get one user by id
        /// </summary>
        /// <returns>One user</returns>
        public Task<User> GetOneByIdAsync(long id);
    }
}
