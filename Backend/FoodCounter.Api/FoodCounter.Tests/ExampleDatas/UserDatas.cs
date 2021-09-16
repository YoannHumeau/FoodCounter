using FoodCounter.Api.Entities;
using FoodCounter.Api.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodCounter.Tests.ExampleDatas
{
    public static class UserDatas
    {
        public static List<string> listUserPasswords = new List<string>
        {
            "123456",
            "123456",
            "123456",
            "Pa$$w0rd"
        };

        public static string UserPasswordToUpdate = "PasswordUpdate";

        public static List<User> listUsers = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "wayne",
                Email = "wayne@party.time",
                Password = BCrypt.Net.BCrypt.HashPassword(listUserPasswords.ElementAt(0)),
                Role = Role.Admin
            },
            new User
            {
                Id = 2,
                Username = "garth",
                Email = "garth.algar@party.time",
                Password = BCrypt.Net.BCrypt.HashPassword(listUserPasswords.ElementAt(1)),
                Role = Role.Admin
            },
            new User
            {
                Id = 3,
                Username = "benjamin",
                Email = "benjamin@noahs.arcade",
                Password = BCrypt.Net.BCrypt.HashPassword(listUserPasswords.ElementAt(2)),
                Role = Role.User
            },
            new User
            {
                Id = 4,
                Username = "cassandra",
                Email = "cassandra@party.time",
                Password = BCrypt.Net.BCrypt.HashPassword(listUserPasswords.ElementAt(3)),
                Role = Role.User
            }
        };

        public static List<UserFullDto> listUsersFullDto = new List<UserFullDto>
        {
            new UserFullDto
            {
                Id = listUsers.ElementAt(0).Id,
                Username = listUsers.ElementAt(0).Username,
                Email = listUsers.ElementAt(0).Email,
                Role = listUsers.ElementAt(0).Role
            },
            new UserFullDto
            {
                Id = listUsers.ElementAt(1).Id,
                Username = listUsers.ElementAt(1).Username,
                Email = listUsers.ElementAt(1).Email,
                Role = listUsers.ElementAt(1).Role
            },
            new UserFullDto
            {
                Id = listUsers.ElementAt(2).Id,
                Username = listUsers.ElementAt(2).Username,
                Email = listUsers.ElementAt(2).Email,
                Role = listUsers.ElementAt(2).Role
            },
            new UserFullDto
            {
                Id = listUsers.ElementAt(3).Id,
                Username = listUsers.ElementAt(3).Username,
                Email = listUsers.ElementAt(3).Email,
                Role = listUsers.ElementAt(3).Role
            }
        };

        public static List<UserLimitedDto> listUsersLimitedDto = new List<UserLimitedDto>
        {
            new UserLimitedDto
            {
                Id = listUsers.ElementAt(0).Id,
                Username = listUsers.ElementAt(0).Username
            },
            new UserLimitedDto
            {
                Id = listUsers.ElementAt(1).Id,
                Username = listUsers.ElementAt(1).Username
            },
            new UserLimitedDto
            {
                Id = listUsers.ElementAt(2).Id,
                Username = listUsers.ElementAt(2).Username
            },
            new UserLimitedDto
            {
                Id = listUsers.ElementAt(3).Id,
                Username = listUsers.ElementAt(3).Username
            }
        };

        public static User newUser = new User
        {
            Username = "frankie",
            Email = "frankie@sharp.records",
            Password = "123456",
        };

        public static User newUserCreated = new User
        {
            Id = 5,
            Username = newUser.Username,
            Email = newUser.Email,
            Password = newUser.Password,
            Role = Role.User
        };

        public static UserFullDto newUserCreatedFullDto = new UserFullDto
        {
            Id = newUserCreated.Id,
            Username = newUserCreated.Username,
            Email = newUserCreated.Email,
            Role = newUserCreated.Role
        };

        public static UserCreationDto newUserCreationModelDto = new UserCreationDto
        {
            Username = newUser.Username,
            Password = newUser.Password
        };

        public static UserUpdatePasswordDto userUpdatePassword = new UserUpdatePasswordDto
        {
            Password = UserPasswordToUpdate
        };

        public static User userUpdatedPassword = new User
        {
            Id = listUsers.ElementAt(3).Id,
            Username = listUsers.ElementAt(3).Username,
            Email = listUsers.ElementAt(3).Email,
            Password = BCrypt.Net.BCrypt.HashPassword(UserPasswordToUpdate), // Use another password
            Role = listUsers.ElementAt(3).Role
        };
    }
}
