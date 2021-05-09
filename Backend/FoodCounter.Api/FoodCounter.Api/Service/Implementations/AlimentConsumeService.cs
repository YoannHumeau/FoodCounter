using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service.Implementations
{
    /// <summary>
    /// AlimentConsume service class
    /// </summary>
    public class AlimentConsumeService : IAlimentConsumeService
    {
        private IAlimentConsumeRepository _alimentConsumeRepository;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="alimentConsumeRepository"></param>
        public AlimentConsumeService(IAlimentConsumeRepository alimentConsumeRepository)
        {
            _alimentConsumeRepository = alimentConsumeRepository;
        }

        ///<inheritdoc/>
        public async Task<AlimentConsume> GetOneByIdAsync(long id)
        {
            var result = await _alimentConsumeRepository.GetOneByIdAsync(id);

            return result;
        }
    }
}
