using FoodCounter.Api.Exceptions;
using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Services.Implementations
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

            if (result == null)
                throw new HttpNotFoundException(ResourceEn.AlimentNotFound);

            return result;
        }

        ///<inheritdoc/>
        public async Task<Aliment> GetOneByNameAsync(string name)
        {
            var result = await _alimentRepository.GetOneByNameAsync(name);

            return result;
        }

        ///<inheritdoc/>
        public async Task<Aliment> UpdateAsync(long id, Aliment newAliment)
        {
            // Check aliment exists : Will throw exception from GetOneByIdAsync() does not exists
            await GetOneByIdAsync(id);

            var result = await _alimentRepository.UpdateAsync(newAliment);

            return result;
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteAsync(long id)
        {
            // Check aliment exists : Will throw exception from GetOneByIdAsync() does not exists
            await GetOneByIdAsync(id);

            var result = await _alimentRepository.DeleteAsync(id);

            return result;
        }
    }
}
