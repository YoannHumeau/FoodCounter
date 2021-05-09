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
        public Task<Aliment> CreateAsync(Aliment newAliment);

        /// <summary>
        /// Get all the aliments
        /// </summary>
        /// <returns>List of aliments</returns>
        public Task<IEnumerable<Aliment>> GetAllAsync();

        /// <summary>
        /// Get one aliment by id
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<Aliment> GetOneByIdAsync(long id);

        /// <summary>
        /// Get one aliment by name
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<Aliment> GetOneByNameAsync(string name);

        /// <summary>
        /// Update an aliment
        /// </summary>
        /// <returns>Aliment created</returns>
        public Task<Aliment> UpdateAsync(Aliment newAliment);

        /// <summary>
        /// Delete one aliment by id
        /// </summary>
        /// <returns>Boolean</returns>
        public Task<bool> DeleteAsync(long id);
    }
}
