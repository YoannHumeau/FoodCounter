using FoodCounter.Api.Entities;
using FoodCounter.Api.Models.Dto;
using System.Collections.Generic;

namespace FoodCounter.Tests.ExampleDatas
{
    public static class UserDatas
    {
        public static List<User> listUsers = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "wayne",
                Password = "123456",
                Role = Role.Admin
            },
            new User
            {
                Id = 2,
                Username = "garth",
                Password = "123456",
                Role = Role.Admin
            },
            new User
            {
                Id = 3,
                Username = "benjamin",
                Password = "123456",
                Role = Role.User
            }
        };

        public static User newUser = new User
        {
            Username = "cassandra",
            Password = "123456",
        };

        public static UserCreationModelDto newUserCreationModelDto = new UserCreationModelDto
        {
            Username = newUser.Username,
            Password = newUser.Password
        };

        public static User newUserCreated = new User
        {
            Id = 4,
            Username = newUser.Username,
            Password = newUser.Password,
            Role = Role.User
        };
    }
}
