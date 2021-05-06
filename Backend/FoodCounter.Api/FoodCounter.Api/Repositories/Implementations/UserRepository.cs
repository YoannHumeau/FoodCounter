using Dommel;
using FoodCounter.Api.DataAccess.DataAccess;
using FoodCounter.Api.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCounter.Api.Repositories.Implementations
{
    /// <summary>
    /// User repository class
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="db">Db Access</param>
        public UserRepository(DbAccess db)
        {
            _connection = db.Connection;
        }

        /// <inheritdoc/>
        public async Task<User> CreateAsync(User newUser)
        {
            var resultCreationId = await _connection.InsertAsync<User>(newUser);

            newUser.Id = Convert.ToInt64(resultCreationId);

            return newUser;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _connection.GetAllAsync<User>();

            return result;
        }

        /// <inheritdoc/>
        public async Task<User> GetOneByIdAsync(long id)
        {
            var result = await _connection.GetAsync<User>(id);

            return result;
        }
    }
}
