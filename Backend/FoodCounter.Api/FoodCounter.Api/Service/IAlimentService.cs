using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service
{
    public interface IAlimentService
    {
        public IEnumerable<AlimentModel> GetAll();

        public Task<AlimentModel> GetByIdAsync(int id);
    }
}
