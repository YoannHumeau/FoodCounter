using Dommel;
using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Repositories.Implementations
{
    public class AlimentRepository : BaseRepository, IAlimentRepository
    {
        public IEnumerable<Models.AlimentModel> GetAll()
        {
            var result = _connection.GetAll<AlimentModel>();

            return result;
        }
    }
}
