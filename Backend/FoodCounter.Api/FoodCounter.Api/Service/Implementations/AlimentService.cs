using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using System.Collections.Generic;

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
    }
}
