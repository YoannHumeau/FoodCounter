using Dommel;
using FoodCounter.Api.DataAccess.DataAccess;
using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

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
        public async Task<IEnumerable<Models.AlimentConsume>> GetAllByUserIdAsync(long userId)
        {
            // Select does not permit navigation
            //var result = await _connection.SelectAsync<AlimentConsume>(ac => ac.UserId == userId);

            // TODO : This method works but load all, find a way to use it instead sql query
            //var result0 = (await _connection.GetAllByUserIdAsync<AlimentConsume, Aliment, AlimentConsume>()).Where(x => x.UserId == userId);

            var sql = @"
                    SELECT * FROM AlimentConsumes ac 
                    INNER JOIN Aliments a ON a.Id = ac.AlimentId
                    WHERE ac.UserId = @UserId ";

            var result = await _connection.QueryAsync<AlimentConsume, Aliment, AlimentConsume>
                (sql, (ac, a) => { ac.Aliment = a; return ac; }, new { UserId = userId} );
            
            return result;
        }

        /// <inheritdoc/>
        public async Task<AlimentConsume> GetOneByIdAsync(long id)
        {
            var result = await _connection.GetAsync<AlimentConsume, Aliment, AlimentConsume>(id);

            return result;
        }
    }
}
