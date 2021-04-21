﻿using FoodCounter.Api.Models;
using FoodCounter.Api.Models.Dto;
using System.Collections.Generic;

namespace FoodCounter.Tests.ExampleDatas
{
    public static class AlimentDatas
    {
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
    }
}
