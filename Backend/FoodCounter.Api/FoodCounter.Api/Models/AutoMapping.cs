﻿using AutoMapper;
using FoodCounter.Api.Models.Dto;

namespace FoodCounter.Api.Models
{
    /// <summary>
    /// Mapping class 
    /// </summary>
    public class AutoMapping : Profile
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public AutoMapping()
        {
            // Tip : CreateMap < TSource, TDestination 

            CreateMap<AlimentCreationModelDto, AlimentModel>();
        }
    }
}