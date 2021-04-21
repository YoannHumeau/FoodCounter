using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Repositories
{
    /// <summary>
    /// Aliment repository class
    /// </summary>
    public interface IAlimentRepository
    {
        /// <summary>
        /// Create aliment
        /// </summary>
        /// <returns>Aliment created</returns>
        public Task<AlimentModel> CreateAsync(AlimentModel newAliment);

        /// <summary>
        /// Get all the aliments
        /// </summary>
        /// <returns>List of aliments</returns>
        public Task<IEnumerable<AlimentModel>> GetAllAsync();

        /// <summary>
        /// Get one aliment by id
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<AlimentModel> GetOneByIdAsync(long id);

        /// <summary>
        /// Get one aliment by id
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<AlimentModel> GetOneByNameAsync(string name);

        /// <summary>
        /// Update an aliment
        /// </summary>
        /// <returns>Aliment created</returns>
        public Task<AlimentModel> UpdateAsync(AlimentModel newAliment);

        /// <summary>
        /// Delete one aliment by id
        /// </summary>
        /// <returns>Boolean</returns>
        public Task<bool> DeleteAsync(long id);
    }
}
