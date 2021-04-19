using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service.Implementations
{
    public class AlimentService : IAlimentService
    {
        private IAlimentRepository _alimentRepository;

        public AlimentService(IAlimentRepository alimentRepository)
        {
            _alimentRepository = alimentRepository;
        }

        public async Task<IEnumerable<AlimentModel>> GetAllAsync()
        {
            var result = await _alimentRepository.GetAllAsync();

            return result;
        }
    }
}
