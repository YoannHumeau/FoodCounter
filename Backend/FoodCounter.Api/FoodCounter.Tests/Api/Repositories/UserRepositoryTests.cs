﻿using FluentAssertions;
using FoodCounter.Api.Repositories.Implementations;
using FoodCounter.Tests.ExampleDatas;
using Xunit;
using System.Linq;

namespace FoodCounter.Tests.Api.Repositories
{
    public class UserRepositoryTests : BaseRepositoryTests
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _userRepository = new UserRepository(_dbAccess);
        }

        [Fact]
        public async void CreateUser_Ok()
        {
            PrepareDatabase();

            var result = await _userRepository.CreateAsync(UserDatas.newUser);

            result.Should().BeEquivalentTo(UserDatas.newUser);

            // TODO : Check the user created by it's Id.
        }

        [Fact]
        public async void GetOneUserById_Ok()
        {
            PrepareDatabase();

            int id = 2;

            var result = await _userRepository.GetOneByIdAsync(id);

            result.Should().BeEquivalentTo(UserDatas.listUsers.ElementAt(id - 1));
        }

        [Fact]
        public async void GetOneUserById_Bad_NotFound()
        {
            PrepareDatabase();

            int id = 777;

            var result = await _userRepository.GetOneByIdAsync(id);

            result.Should().BeNull();
        }
    }
}