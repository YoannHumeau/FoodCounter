using FoodCounter.Api.Models;
using FoodCounter.Api.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodCounter.Tests.ExampleDatas
{
    public static class AlimentConsumeDatas
    {
        public static List<AlimentConsume> listAlimentConsumes = new List<AlimentConsume>
        {
            new AlimentConsume
            {
                Id = 1,
                UserId = 3,
                AlimentId = 1,
                Aliment =  AlimentDatas.listAliments.ElementAt(0),
                ConsumeDate = new DateTime(2021, 05, 08, 13, 37, 00),
                Weight = 100
            },
            new AlimentConsume
            {
                Id = 2,
                UserId = 3,
                AlimentId = 1,
                Aliment =  AlimentDatas.listAliments.ElementAt(0),
                ConsumeDate = new DateTime(2021, 05, 08, 13, 37, 00),
                Weight = 200
            },
            new AlimentConsume
            {
                Id = 3,
                UserId = 3,
                AlimentId = 2,
                Aliment =  AlimentDatas.listAliments.ElementAt(1),
                ConsumeDate = new DateTime(2021, 05, 08, 15, 37, 00),
                Weight = 110
            },
            new AlimentConsume
            {
                Id = 4,
                UserId = 3,
                AlimentId = 2,
                Aliment =  AlimentDatas.listAliments.ElementAt(1),
                ConsumeDate = new DateTime(2021, 05, 08, 15, 39, 00),
                Weight = 220
            },
            new AlimentConsume
            {
                Id = 5,
                UserId = 1,
                AlimentId = 2,
                Aliment =  AlimentDatas.listAliments.ElementAt(1),
                ConsumeDate = new DateTime(2021, 05, 08, 15, 39, 00),
                Weight = 222
            },
            new AlimentConsume
            {
                Id = 6,
                UserId = 1,
                AlimentId = 2,
                Aliment =  AlimentDatas.listAliments.ElementAt(1),
                ConsumeDate = new DateTime(2021, 05, 08, 15, 39, 00),
                Weight = 333
            },
        };

        public static List<AlimentConsumeDto> listAlimentConsumesDto = new List<AlimentConsumeDto>
        {
            new AlimentConsumeDto
            {
                Id = listAlimentConsumes.ElementAt(0).Id,
                Aliment =  listAlimentConsumes.ElementAt(0).Aliment,
                ConsumeDate = listAlimentConsumes.ElementAt(0).ConsumeDate,
                Weight = listAlimentConsumes.ElementAt(0).Weight
            },
            new AlimentConsumeDto
            {
                Id = listAlimentConsumes.ElementAt(1).Id,
                Aliment =  listAlimentConsumes.ElementAt(1).Aliment,
                ConsumeDate = listAlimentConsumes.ElementAt(1).ConsumeDate,
                Weight = listAlimentConsumes.ElementAt(1).Weight
            },
            new AlimentConsumeDto
            {
                Id = listAlimentConsumes.ElementAt(2).Id,
                Aliment =  listAlimentConsumes.ElementAt(2).Aliment,
                ConsumeDate = listAlimentConsumes.ElementAt(2).ConsumeDate,
                Weight = listAlimentConsumes.ElementAt(2).Weight
            },
            new AlimentConsumeDto
            {
                Id = listAlimentConsumes.ElementAt(3).Id,
                Aliment =  listAlimentConsumes.ElementAt(3).Aliment,
                ConsumeDate = listAlimentConsumes.ElementAt(3).ConsumeDate,
                Weight = listAlimentConsumes.ElementAt(3).Weight
            },
            new AlimentConsumeDto
            {
                Id = listAlimentConsumes.ElementAt(4).Id,
                Aliment =  listAlimentConsumes.ElementAt(4).Aliment,
                ConsumeDate = listAlimentConsumes.ElementAt(4).ConsumeDate,
                Weight = listAlimentConsumes.ElementAt(4).Weight
            },
            new AlimentConsumeDto
            {
                Id = listAlimentConsumes.ElementAt(5).Id,
                Aliment =  listAlimentConsumes.ElementAt(5).Aliment,
                ConsumeDate = listAlimentConsumes.ElementAt(5).ConsumeDate,
                Weight = listAlimentConsumes.ElementAt(5).Weight
            },
        };

        public static AlimentConsume newAlimentConsume = new AlimentConsume
        {
            UserId = 3,
            AlimentId = 2,
            ConsumeDate = new DateTime(2021, 05, 09, 23, 01, 00),
            Weight = 111
        };

        public static AlimentConsume newAlimentConsumeCreated = new AlimentConsume
        {
            Id = 7,
            UserId = newAlimentConsume.UserId,
            AlimentId = newAlimentConsume.AlimentId,
            Aliment = listAlimentConsumes.ElementAt((int)newAlimentConsume.AlimentId).Aliment,
            ConsumeDate = newAlimentConsume.ConsumeDate,
            Weight = newAlimentConsume.Weight
        };

        public static AlimentConsumeCreationDto newAlimentConsumeCreationDto = new AlimentConsumeCreationDto
        {
            AlimentId = newAlimentConsume.AlimentId,
            ConsumeDate = newAlimentConsume.ConsumeDate,
            Weight = newAlimentConsume.Weight
        };

        public static AlimentConsumeDto newAlimentConsumeCreatedDto = new AlimentConsumeDto
        {
            Id = newAlimentConsumeCreated.Id,
            Aliment = newAlimentConsumeCreated.Aliment,
            ConsumeDate = newAlimentConsumeCreated.ConsumeDate,
            Weight = newAlimentConsumeCreated.Weight
        };

        public static AlimentConsumeUpdateDto updateAlimentConsumeUpdateDto = new AlimentConsumeUpdateDto
        {
            Weight = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).Weight + 111
        };

        public static AlimentConsume afterUpdateAlimentConsume = new AlimentConsume
        {
            Id = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).Id,
            UserId = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).UserId,
            AlimentId = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).AlimentId,
            Aliment = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).Aliment,
            ConsumeDate = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).ConsumeDate,
            Weight = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).Weight + 111
        };

        public static AlimentConsumeDto afterUpdateAlimentConsumeDto = new AlimentConsumeDto
        {
            Id = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).Id,
            Aliment = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).Aliment,
            ConsumeDate = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).ConsumeDate,
            Weight = AlimentConsumeDatas.listAlimentConsumes.ElementAt(1).Weight + 111
        };
    }
}
