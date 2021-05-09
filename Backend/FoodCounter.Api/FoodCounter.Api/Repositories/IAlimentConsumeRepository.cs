﻿using FoodCounter.Api.Models;
using System.Threading.Tasks;

namespace FoodCounter.Api.Repositories
{
    public interface IAlimentConsumeRepository
    {
        /// <summary>
        /// Get one aliment consume by id
        /// </summary>
        /// <returns>One aliment</returns>
        public Task<AlimentConsume> GetOneByIdAsync(long id);
    }
}