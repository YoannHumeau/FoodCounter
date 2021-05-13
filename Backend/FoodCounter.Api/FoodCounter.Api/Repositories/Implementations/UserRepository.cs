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
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

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

        /// <inheritdoc/>
        public async Task<User> GetOneByUsernameAndPassword(string username, string password)
        {
            var result = (await _connection.SelectAsync<User>(u => u.Username == username && u.Password == password)).FirstOrDefault();

            return result;

            // Make tests here
        }

        /// <inheritdoc/>
        public async Task<User> GetOneByUsernameAsync(string username)
        {
            var result = (await _connection.SelectAsync<User>(u => u.Username == username)).FirstOrDefault();

            return result;
        }

        /// <inheritdoc/>
        public async Task<User> GetOneByEmailAsync(string email)
        {
            var result = (await _connection.SelectAsync<User>(u => u.Email == email)).FirstOrDefault();

            return result;
        }
    }
}
