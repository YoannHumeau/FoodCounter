using FoodCounter.Api.Entities;
using System.Threading.Tasks;

namespace FoodCounter.Api.Repositories
{
    /// <summary>
    /// User repository class
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <returns>User created</returns>
        public Task<User> CreateAsync(User newUser);

        /// <summary>
        /// Get one user by id
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<User> GetOneByIdAsync(long id);
    }
}
