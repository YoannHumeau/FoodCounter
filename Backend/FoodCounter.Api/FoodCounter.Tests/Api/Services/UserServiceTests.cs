using FluentAssertions;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using System.Linq;
using FoodCounter.Tests.ExampleDatas;
using FoodCounter.Api.Entities;

namespace FoodCounter.Tests.Api.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepository;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async void GetOneAlimentById_Ok()
        {
            int id = 2;

            _mockUserRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(UserDatas.listUsers.ElementAt(id - 1));

            var result = await _userService.GetOneByIdAsync(id);

            result.Should().BeEquivalentTo(UserDatas.listUsers.ElementAt(id - 1));

            _mockUserRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentById_Bad_NotFound()
        {
            int id = 777;

            _mockUserRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);

            var result = await _userService.GetOneByIdAsync(id);

            result.Should().BeNull();

            _mockUserRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }
    }
}
