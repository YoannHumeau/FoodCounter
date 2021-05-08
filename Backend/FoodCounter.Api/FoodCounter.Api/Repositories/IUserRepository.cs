﻿using FoodCounter.Api.Entities;
using System.Collections.Generic;
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
        /// Get all users
        /// </summary>
        /// <returns>List of users</returns>
        public Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Get one user by id
        /// </summary>
        /// <returns>One user</returns>
        public Task<User> GetOneByIdAsync(long id);

        /// <summary>
        /// Get one user by username and password
        /// </summary>
        /// <returns>One user</returns>
        public Task<User> GetOneByUsernameAndPassword(string username, string password);
    }
}
