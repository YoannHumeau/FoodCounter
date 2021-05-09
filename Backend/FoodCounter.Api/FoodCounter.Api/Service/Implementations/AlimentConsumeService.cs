﻿using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using System.Collections.Generic;
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

        ///<inheritdoc/>
        public async Task<IEnumerable<AlimentConsume>> GetAllByUserIdAsync(long userId)
        {
            var result = await _alimentConsumeRepository.GetAllByUserIdAsync(userId);

            return result;
        }
    }
}
