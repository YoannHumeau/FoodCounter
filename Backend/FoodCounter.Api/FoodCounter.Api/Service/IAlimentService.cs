using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service
{
    public interface IAlimentService
    {
        public Task<IEnumerable<AlimentModel>> GetAllAsync();
    }
}
