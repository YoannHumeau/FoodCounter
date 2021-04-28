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
        /// Get one user by id
        /// </summary>
        /// <returns>One user</returns>
        public Task<User> GetOneByIdAsync(long id);
    }
}
