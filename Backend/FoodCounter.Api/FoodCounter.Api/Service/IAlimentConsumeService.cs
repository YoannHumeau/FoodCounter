using FoodCounter.Api.Models;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service
{
    /// <summary>
    /// AlimentConsume service class
    /// </summary>
    public interface IAlimentConsumeService
    {
        /// <summary>
        /// Get one aliment consume by id
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<AlimentConsume> GetOneByIdAsync(long id);
    }
}
