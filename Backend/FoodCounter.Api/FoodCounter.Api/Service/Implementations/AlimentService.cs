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
        public async Task<Aliment> CreateAsync(Aliment newAliment)
        {
            var result = await _alimentRepository.CreateAsync(newAliment);

            return result;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<Aliment>> GetAllAsync()
        {
            var result = await _alimentRepository.GetAllAsync();

            return result;
        }

        ///<inheritdoc/>
        public async Task<Aliment> GetOneByIdAsync(long id)
        {
            var result = await _alimentRepository.GetOneByIdAsync(id);

            return result;
        }

        ///<inheritdoc/>
        public async Task<Aliment> GetOneByNameAsync(string name)
        {
            var result = await _alimentRepository.GetOneByNameAsync(name);

            return result;
        }

        ///<inheritdoc/>
        public async Task<Aliment> UpdateAsync(Aliment newAliment)
        {
            var result = await _alimentRepository.UpdateAsync(newAliment);

            return result;
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteAsync(long id)
        {
            var result = await _alimentRepository.DeleteAsync(id);

            return result;
        }
    }
}
