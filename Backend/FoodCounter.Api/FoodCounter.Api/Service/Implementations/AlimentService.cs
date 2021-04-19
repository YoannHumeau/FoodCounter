using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<AlimentModel> GetAll()
        {
            var result = _alimentRepository.GetAll();

            return result;
        }

        public async Task<AlimentModel> GetByIdAsync(int id)
        {
            var result = await _alimentRepository.GetByIdAsync(id);

            return result;
        }
    }
}
