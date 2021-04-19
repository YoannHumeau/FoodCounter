using FoodCounter.Api.Models;
using System.Collections.Generic;

namespace FoodCounter.Api.Service
{
    public interface IAlimentService
    {
        public IEnumerable<AlimentModel> GetAll();
    }
}
