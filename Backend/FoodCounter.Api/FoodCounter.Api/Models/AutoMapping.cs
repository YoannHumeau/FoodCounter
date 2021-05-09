using AutoMapper;
using FoodCounter.Api.Entities;
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
            // Tip : CreateMap < TSource, TDestination > ()

            CreateMap<AlimentCreationDto, Aliment>();
            CreateMap<AlimentUpdateDto, Aliment>();

            CreateMap<AlimentConsume, AlimentConsumeDto>();
            CreateMap<AlimentConsumeCreationDto, AlimentConsume>();

            CreateMap<UserCreationDto, User>();
            CreateMap<User, UserLoggedDto>();
            CreateMap<User, UserFullDto>();
        }
    }
}
