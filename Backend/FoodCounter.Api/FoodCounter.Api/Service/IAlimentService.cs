﻿using FoodCounter.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Service
{
    /// <summary>
    /// Aliment service class
    /// </summary>
    public interface IAlimentService
    {
        /// <summary>
        /// Get all the aliments
        /// </summary>
        /// <returns>List of aliments</returns>
        public Task<IEnumerable<AlimentModel>> GetAllAsync();

        /// <summary>
        /// Get one aliment by id
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<AlimentModel> GetOneByIdAsync(long id);
    }
}