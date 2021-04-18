using FoodCounter.Api.Models;
using System.Collections.Generic;

namespace FoodCounter.Api.Repositories
{
    /// <summary>
    /// Aliment repository
    /// </summary>
    public interface IAlimentRepository
    {
        /// <summary>
        /// Gett all the aliments
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AlimentModel> GetAll();
    }
}
