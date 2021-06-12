using FoodCounter.Api.Models;
using FoodCounter.Api.Models.Dto;
using System.Collections.Generic;
using System.Linq;

namespace FoodCounter.Tests.ExampleDatas
{
    public static class AlimentDatas
    {
        public static List<Aliment> listAliments = new List<Aliment>
        {
            new Aliment
            {
                Id = 1,
                Name = "Aliment 1",
                Calories = 100,
                Barecode = null
            },
            new Aliment
            {
                Id = 2,
                Name = "Aliment 2",
                Calories = 222,
                Barecode = null
            },
            new Aliment
            {
                Id = 3,
                Name = "Aliment 3",
                Calories = 300,
                Barecode = null
            },
            new Aliment
            {
                Id = 4,
                Name = "Aliment 4",
                Calories = 400,
                Barecode = "1234567890123"
            },
            new Aliment
            {
                Id = 5,
                Name = "Aliment 5",
                Calories = 555,
                Barecode = "1234567890456"
            },
            new Aliment
            {
                Id = 6,
                Name = "Aliment 6",
                Calories = 600,
                Barecode = "1234567890"
            },
            new Aliment
            {
                Id = 7,
                Name = "Aliment 7",
                Calories = 777,
                Barecode = "1234567890"
            }
        };

        public static List<AlimentDto> listAlimentsDto = new List<AlimentDto>
        {
            new AlimentDto 
            {
                Id = listAliments.ElementAt(0).Id,
                Name = listAliments.ElementAt(0).Name,
                Calories = listAliments.ElementAt(0).Calories,
                Barecode = listAliments.ElementAt(0).Barecode
            },
            new AlimentDto
            {
                Id = listAliments.ElementAt(1).Id,
                Name = listAliments.ElementAt(1).Name,
                Calories = listAliments.ElementAt(1).Calories,
                Barecode = listAliments.ElementAt(1).Barecode
            },
            new AlimentDto
            {
                Id = listAliments.ElementAt(2).Id,
                Name = listAliments.ElementAt(2).Name,
                Calories = listAliments.ElementAt(2).Calories,
                Barecode = listAliments.ElementAt(2).Barecode
            },
            new AlimentDto
            {
                Id = listAliments.ElementAt(3).Id,
                Name = listAliments.ElementAt(3).Name,
                Calories = listAliments.ElementAt(3).Calories,
                Barecode = listAliments.ElementAt(3).Barecode
            },
            new AlimentDto
            {
                Id = listAliments.ElementAt(4).Id,
                Name = listAliments.ElementAt(4).Name,
                Calories = listAliments.ElementAt(4).Calories,
                Barecode = listAliments.ElementAt(4).Barecode
            },
            new AlimentDto
            {
                Id = listAliments.ElementAt(5).Id,
                Name = listAliments.ElementAt(5).Name,
                Calories = listAliments.ElementAt(5).Calories,
                Barecode = listAliments.ElementAt(5).Barecode
            },
            new AlimentDto
            {
                Id = listAliments.ElementAt(6).Id,
                Name = listAliments.ElementAt(6).Name,
                Calories = listAliments.ElementAt(6).Calories,
                Barecode = listAliments.ElementAt(6).Barecode
            },
        };

        public static Aliment newAliment = new Aliment
        {
            Name = "Aliment 8",
            Calories = 123,
            Barecode = null
        };

        public static AlimentCreationDto newAlimentCreationDto = new AlimentCreationDto
        {
            Name = newAliment.Name,
            Calories = newAliment.Calories,
            Barecode = newAliment.Barecode
        };

        public static Aliment newAlimentDto = new Aliment
        {
            Id = 8,
            Name = newAliment.Name,
            Calories = newAliment.Calories,
            Barecode = newAliment.Barecode
        };

        public static Aliment updateAliment = new Aliment
        {
            Id = 5,
            Name = "Aliment 5 Update",
            Calories = 555,
            Barecode = "1234567890555"
        };

        public static AlimentUpdateDto updateAlimentUpdateDto = new AlimentUpdateDto
        {
            Name = updateAliment.Name,
            Calories = updateAliment.Calories,
            Barecode = updateAliment.Barecode
        };
    }
}
