using Dommel;
using FoodCounter.Api.DataAccess.DataAccess;
using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace FoodCounter.Api.Repositories.Implementations
{
    /// <summary>
    /// Aliment repository class
    /// </summary>
    public class AlimentRepository : IAlimentRepository
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="db">Db Access</param>
        public AlimentRepository(DbAccess db)
        {
            _connection = db.Connection;
        }

        /// <inheritdoc/>
        public async Task<AlimentModel> CreateAsync(AlimentModel newAliment)
        {
            var resultCreationId = await _connection.InsertAsync<AlimentModel>(newAliment);

            newAliment.Id = Convert.ToInt64(resultCreationId);

            return newAliment;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Models.AlimentModel>> GetAllAsync()
        {
            var result = await _connection.GetAllAsync<AlimentModel>();

            return result;
        }

        /// <inheritdoc/>
        public async Task<AlimentModel> GetOneByIdAsync(long id)
        {
            var result = await _connection.GetAsync<AlimentModel>(id);

            return result;
        }

        /// <inheritdoc/>
        public async Task<AlimentModel> GetOneByNameAsync(string name)
        {
            var result = (await _connection.SelectAsync<AlimentModel>(s => s.Name == name)).FirstOrDefault();

            return result;
        }
    }
}
