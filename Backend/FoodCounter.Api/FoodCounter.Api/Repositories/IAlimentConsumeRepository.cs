using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Repositories
{
    /// <summary>
    /// Aliment Consume repository class
    /// </summary>
    public interface IAlimentConsumeRepository
    {
        /// <summary>
        /// Create aliment consunme
        /// </summary>
        /// <returns>Aliment created</returns>
        public Task<AlimentConsume> CreateAsync(AlimentConsume newAlimentConsume);

        /// <summary>
        /// Get one aliment consume by id
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<AlimentConsume> GetOneByIdAsync(long id);

        /// <summary>
        /// Get all the aliments consume for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of aliments consumes</returns>
        public Task<IEnumerable<AlimentConsume>> GetAllByUserIdAsync(long userId);
    }
}
