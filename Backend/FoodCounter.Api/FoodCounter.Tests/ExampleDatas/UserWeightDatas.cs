using System;
using System.Collections.Generic;
using FoodCounter.Api.Models;

namespace FoodCounter.Tests.ExampleDatas
{
    public static class UserWeightDatas
    {
        public static List<UserWeight> listUserWeight = new List<UserWeight>
        {
            new UserWeight
            {
                Id = 1,
                UserId = 1,
                WeightDate = new DateTime(2021, 04, 07),
                Weight = 95400
            },
            new UserWeight
            {
                Id = 2,
                UserId = 1,
                WeightDate = new DateTime(2021, 04, 08),
                Weight = 95300
            },
            new UserWeight
            {
                Id = 3,
                UserId = 1,
                WeightDate = new DateTime(2021, 04, 09),
                Weight = 95000
            },
            new UserWeight
            {
                Id = 1,
                UserId = 1,
                WeightDate = new DateTime(2021, 04, 11),
                Weight = 94500
            },
            new UserWeight
            {
                Id = 1,
                UserId = 3,
                WeightDate = new DateTime(2021, 04, 01),
                Weight = 78000
            },
            new UserWeight
            {
                Id = 1,
                UserId = 1,
                WeightDate = new DateTime(2021, 04, 12),
                Weight = 94000
            },
            new UserWeight
            {
                Id = 1,
                UserId = 3,
                WeightDate = new DateTime(2021, 04, 14),
                Weight = 78300
            },
            new UserWeight
            {
                Id = 1,
                UserId = 1,
                WeightDate = new DateTime(2021, 04, 15),
                Weight = 93000
            },
            new UserWeight
            {
                Id = 1,
                UserId = 1,
                WeightDate = new DateTime(2021, 04, 18),
                Weight = 93900
            },
            new UserWeight
            {
                Id = 1,
                UserId = 1,
                WeightDate = new DateTime(2021, 04, 19),
                Weight = 93500
            }
        };
    }
}
