using Dommel;
using FoodCounter.Api.DataAccess.DataAccess;
using FoodCounter.Api.Models;
using System.Data;
using System.Threading.Tasks;

namespace FoodCounter.Api.Repositories.Implementations
{
    /// <inheritdoc/>
    public class AlimentConsumeRepository : IAlimentConsumeRepository
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="db">Db Access</param>
        public AlimentConsumeRepository(DbAccess db)
        {
            _connection = db.Connection;
        }

        /// <inheritdoc/>
        public async Task<AlimentConsume> GetOneByIdAsync(long id)
        {
            var result = await _connection.GetAsync<AlimentConsume, Aliment, AlimentConsume>(id);

            return result;
        }
    }
}
