using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service
{
    /// <summary>
    /// Aliment service class
    /// </summary>
    public interface IAlimentService
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
        /// Get one aliment by name
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<AlimentModel> GetOneByNameAsync(string name);
    }
}
