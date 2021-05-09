using FoodCounter.Api.Models;
using FoodCounter.Api.Models.Dto;
using System;
using System.Collections.Generic;

namespace FoodCounter.Tests.ExampleDatas
{
    public static class AlimentConsumeDatas
    {
        public static List<AlimentconsumeModel> listAlimentConsumes = new List<AlimentconsumeModel>
        {
            new AlimentconsumeModel
            {
                Id = 1,
                AlimentId = 1,
                UserId = 1,
                ConsumeDate = new DateTime(2021, 05, 08, 13, 37, 00),
                Weight = 100
            },
            new AlimentconsumeModel
            {
                Id = 2,
                AlimentId = 1,
                UserId = 1,
                ConsumeDate = new DateTime(2021, 05, 08, 13, 37, 00),
                Weight = 200
            },
            new AlimentconsumeModel
            {
                Id = 3,
                AlimentId = 2,
                UserId = 1,
                ConsumeDate = new DateTime(2021, 05, 08, 15, 37, 00),
                Weight = 110
            },
            new AlimentconsumeModel
            {
                Id = 4,
                AlimentId = 2,
                UserId = 1,
                ConsumeDate = new DateTime(2021, 05, 08, 15, 39, 00),
                Weight = 220
            },
        };

        public static List<AlimentModel> listAliments = new List<AlimentModel>
        {
            new AlimentModel
            {
                Id = 1,
                Name = "Aliment 1",
                Calories = 100,
                Barecode = null
            },
            new AlimentModel
            {
                Id = 2,
                Name = "Aliment 2",
                Calories = 222,
                Barecode = null
            },
            new AlimentModel
            {
                Id = 3,
                Name = "Aliment 3",
                Calories = 300,
                Barecode = null
            },
            new AlimentModel
            {
                Id = 4,
                Name = "Aliment 4",
                Calories = 400,
                Barecode = "1234567890123"
            },
            new AlimentModel
            {
                Id = 5,
                Name = "Aliment 5",
                Calories = 555,
                Barecode = "1234567890456"
            },
            new AlimentModel
            {
                Id = 6,
                Name = "Aliment 6",
                Calories = 600,
                Barecode = "1234567890"
            },
            new AlimentModel
            {
                Id = 7,
                Name = "Aliment 7",
                Calories = 777,
                Barecode = "1234567890"
            }
        };

        public static AlimentModel newAliment = new AlimentModel
        {
            Name = "Aliment 8",
            Calories = 123,
            Barecode = null
        };

        public static AlimentCreationModelDto newAlimentCreationDto = new AlimentCreationModelDto
        {
            Name = newAliment.Name,
            Calories = newAliment.Calories,
            Barecode = newAliment.Barecode
        };

        public static AlimentModel newAlimentDto = new AlimentModel
        {
            Id = 8,
            Name = newAliment.Name,
            Calories = newAliment.Calories,
            Barecode = newAliment.Barecode
        };

        public static AlimentModel updateAliment = new AlimentModel
        {
            Id = 5,
            Name = "Aliment 5 Update",
            Calories = 555,
            Barecode = "1234567890555"
        };

        public static AlimentUpdateModelDto updateAlimentUpdateDto = new AlimentUpdateModelDto
        {
            Name = updateAliment.Name,
            Calories = updateAliment.Calories,
            Barecode = updateAliment.Barecode
        };
    }
}
