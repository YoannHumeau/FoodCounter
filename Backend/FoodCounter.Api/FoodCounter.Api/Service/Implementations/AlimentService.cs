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
        public async Task<AlimentModel> CreateAsync(AlimentModel newAliment)
        {
            var result = await _alimentRepository.CreateAsync(newAliment);

            return result;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<AlimentModel>> GetAllAsync()
        {
            var result = await _alimentRepository.GetAllAsync();

            return result;
        }

        ///<inheritdoc/>
        public async Task<AlimentModel> GetOneByIdAsync(long id)
        {
            var result = await _alimentRepository.GetOneByIdAsync(id);

            return result;
        }

        ///<inheritdoc/>
        public async Task<AlimentModel> GetOneByNameAsync(string name)
        {
            var result = await _alimentRepository.GetOneByNameAsync(name);

            return result;
        }
    }
}
