using FoodCounter.Api.Entities;
using System.Collections.Generic;
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
        /// <returns>User created</returns>
        public Task<User> CreateAsync(User newUser);

        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns>List of users</returns>
        public Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Get one user by id
        /// </summary>
        /// <returns>One user</returns>
        public Task<User> GetOneByIdAsync(long id);

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public Task<User> Authenticate(string username, string password);
    }
}
