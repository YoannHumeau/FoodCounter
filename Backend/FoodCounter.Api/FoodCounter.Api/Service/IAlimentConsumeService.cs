using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service
{
    /// <summary>
    /// AlimentConsume service class
    /// </summary>
    public interface IAlimentConsumeService
    {
        /// <summary>
        /// Create aliment consume
        /// </summary>
        /// <returns>Aliment consume created</returns>
        public Task<AlimentConsume> CreateAsync(AlimentConsume newAlimentConsume);

        /// <summary>
        /// Get one aliment consume by id
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<AlimentConsume> GetOneByIdAsync(long id);

        /// <summary>
        /// Get all the aliment consumes for user
        /// </summary>
        /// <returns>List of aliment consumes of the user</returns>
        public Task<IEnumerable<AlimentConsume>> GetAllByUserIdAsync(long userId);

        /// <summary>
        /// Delete one aliment consume by id
        /// </summary>
        /// <returns>Boolean</returns>
        public Task<bool> DeleteAsync(long id);
    }
}
