﻿using FoodCounter.Api.Entities;
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
                Email = "wayne@party.time",
                Password = "123456",
                Role = Role.Admin
            },
            new User
            {
                Id = 2,
                Username = "garth",
                Email = "garth.algar@party.time",
                Password = "123456",
                Role = Role.Admin
            },
            new User
            {
                Id = 3,
                Username = "benjamin",
                Email = "benjamin@noahs.arcade",
                Password = "123456",
                Role = Role.User
            }
        };

        public static User newUser = new User
        {
            Username = "cassandra",
            Email = "cassandra@party.time",
            Password = "123456",
        };

        public static User newUserCreated = new User
        {
            Id = 4,
            Username = newUser.Username,
            Email = newUser.Email,
            Password = newUser.Password,
            Role = Role.User
        };

        public static UserCreationDto newUserCreationModelDto = new UserCreationDto
        {
            Username = newUser.Username,
            Password = newUser.Password
        };
    }
}
