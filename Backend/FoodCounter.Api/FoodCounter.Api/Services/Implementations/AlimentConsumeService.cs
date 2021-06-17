﻿using FoodCounter.Api.Exceptions;
using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodCounter.Api.Services.Implementations
{
    /// <summary>
    /// AlimentConsume service class
    /// </summary>
    public class AlimentConsumeService : IAlimentConsumeService
    {
        private IAlimentConsumeRepository _alimentConsumeRepository;
        private HttpContext _hcontext;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="alimentConsumeRepository"></param>
        public AlimentConsumeService(IAlimentConsumeRepository alimentConsumeRepository, IHttpContextAccessor haccess)
        {
            _alimentConsumeRepository = alimentConsumeRepository;

            _hcontext = haccess.HttpContext;
        }

        ///<inheritdoc/>
        public async Task<AlimentConsume> CreateAsync(AlimentConsume newAlimentConsume)
        {
            var result = await _alimentConsumeRepository.CreateAsync(newAlimentConsume);

            return result;
        }

        ///<inheritdoc/>
        public async Task<AlimentConsume> GetOneByIdAsync(long id)
        {
            var result = await _alimentConsumeRepository.GetOneByIdAsync(id);

            if (result == null)
                throw new HttpNotFoundException(ResourceEn.AlimentConsumeNotFound);

            return result;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<AlimentConsume>> GetAllByUserIdAsync(long userId)
        {
            if (userId != Convert.ToInt64(_hcontext.User.Identity.Name) && !Helpers.IdentityHelper.IsUserAdmin(_hcontext.User))
                throw new HttpForbiddenException(ResourceEn.AccessDenied);

            var result = await _alimentConsumeRepository.GetAllByUserIdAsync(userId);

            return result;
        }

        ///<inheritdoc/>
        public async Task<AlimentConsume> UpdateAsync(AlimentConsume newAlimentConsume)
        {
            var result = await _alimentConsumeRepository.UpdateAsync(newAlimentConsume);

            return result;
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteAsync(long id)
        {
            var result = await _alimentConsumeRepository.DeleteAsync(id);

            return result;
        }
    }
}
