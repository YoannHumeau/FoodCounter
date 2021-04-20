using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service.Implementations
{
    /// <summary>
    /// Aliment service class
    /// </summary>
    public class AlimentService : IAlimentService
    {
        private IAlimentRepository _alimentRepository;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="alimentRepository"></param>
        public AlimentService(IAlimentRepository alimentRepository)
        {
            _alimentRepository = alimentRepository;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<AlimentModel>> GetAllAsync()
        {
            var result = await _alimentRepository.GetAllAsync();

            return result;
        }
    }
}
