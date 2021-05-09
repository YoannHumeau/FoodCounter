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
        public async Task<Aliment> CreateAsync(Aliment newAliment)
        {
            var resultCreationId = await _connection.InsertAsync<Aliment>(newAliment);

            newAliment.Id = Convert.ToInt64(resultCreationId);

            return newAliment;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Models.Aliment>> GetAllAsync()
        {
            var result = await _connection.GetAllAsync<Aliment>();

            return result;
        }

        /// <inheritdoc/>
        public async Task<Aliment> GetOneByIdAsync(long id)
        {
            var result = await _connection.GetAsync<Aliment>(id);

            return result;
        }

        /// <inheritdoc/>
        public async Task<Aliment> GetOneByNameAsync(string name)
        {
            var result = (await _connection.SelectAsync<Aliment>(s => s.Name == name)).FirstOrDefault();

            return result;
        }

        /// <inheritdoc/>
        public async Task<Aliment> UpdateAsync(Aliment updateAliment)
        {
            var aliment = await _connection.GetAsync<Aliment>(updateAliment.Id);
            if (aliment == null)
                return null;

            aliment.Name = updateAliment.Name;
            aliment.Calories = updateAliment.Calories;
            aliment.Barecode = updateAliment.Barecode;

            var result = await _connection.UpdateAsync<Aliment>(aliment);

            if (!result)
                return null;

            return updateAliment;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(long id)
        {
            var aliment = await _connection.GetAsync<Aliment>(id);

            if (aliment == null)
                return false;

            var result = await _connection.DeleteAsync<Aliment>(aliment);

            return result;
        }
    }
}
