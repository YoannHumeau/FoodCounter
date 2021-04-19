using Dommel;
using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Repositories.Implementations
{
    public class AlimentRepository : BaseRepository, IAlimentRepository
    {
        public async Task<IEnumerable<Models.AlimentModel>> GetAllAsync()
        {
            var result = await _connection.GetAllAsync<AlimentModel>();

            return result;
        }
    }
}
