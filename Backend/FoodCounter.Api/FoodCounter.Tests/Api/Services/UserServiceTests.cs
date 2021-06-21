using FluentAssertions;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Services;
using Moq;
using Xunit;
using System.Linq;
using FoodCounter.Tests.ExampleDatas;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Entities;
using FoodCounter.Api.Exceptions;

namespace FoodCounter.Tests.Api.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepository;

        public UserServiceTests()
        {
            // Mock som configuration value
            var inMemorySettings = new Dictionary<string, string> {
                {"Authentication:SecretJwtKey", "If you’re gonna spew, spew into this. - Garth Algar October 28, 1992"}};
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(configuration, _mockUserRepository.Object);
        }

        [Fact]
        public async void CreateUser_Ok()
        {
            _mockUserRepository.Setup(m => m.GetOneByUsernameAsync(UserDatas.newUser.Username)).ReturnsAsync(() => null);
            _mockUserRepository.Setup(m => m.GetOneByEmailAsync(UserDatas.newUser.Email)).ReturnsAsync(() => null);
            _mockUserRepository.Setup(m => m.CreateAsync(UserDatas.newUser)).ReturnsAsync(UserDatas.newUserCreated);

            var result = await _userService.CreateAsync(UserDatas.newUser);

            result.Should().BeEquivalentTo(UserDatas.newUserCreated);

            _mockUserRepository.Verify(m => m.CreateAsync(UserDatas.newUser), Times.Once);
            _mockUserRepository.Verify(m => m.GetOneByEmailAsync(UserDatas.newUser.Email), Times.Once);
            _mockUserRepository.Verify(m => m.GetOneByUsernameAsync(UserDatas.newUser.Username), Times.Once);
        }

        // TODO : Tests for fail user creation (already exists)

        [Fact]
        public void CreateUser_Bad_UsernameAlreadyExists()
        {
            var userExists = UserDatas.listUsers.ElementAt(2);

            var badNewUser = new User
            {
                Username = userExists.Username,
                Email = "new@email.tld",
                Password = "123456"
            };

            _mockUserRepository.Setup(m => m.GetOneByEmailAsync(UserDatas.newUser.Email)).ReturnsAsync(() => null);
            _mockUserRepository.Setup(m => m.GetOneByUsernameAsync(badNewUser.Username)).ReturnsAsync(userExists);

            Assert.ThrowsAsync<HttpConflictException>(() => _userService.CreateAsync(badNewUser))
                .Equals(ResourceEn.UsernameAlreadyExists);

            _mockUserRepository.Verify(m => m.CreateAsync(It.IsAny<User>()), Times.Never);
            _mockUserRepository.Verify(m => m.GetOneByUsernameAsync(badNewUser.Username), Times.Once);
            _mockUserRepository.Verify(m => m.GetOneByEmailAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CreateUser_Bad_EmailAlreadyExists()
        {
            var userExists = UserDatas.listUsers.ElementAt(2);

            var badNewUser = new User
            {
                Username = "newusername",
                Email = userExists.Email,
                Password = "123456"
            };

            _mockUserRepository.Setup(m => m.GetOneByEmailAsync(badNewUser.Email)).ReturnsAsync(userExists);

            Assert.ThrowsAsync<HttpConflictException>(() => _userService.CreateAsync(badNewUser))
                .Equals(ResourceEn.EmailAlreadyExists);

            _mockUserRepository.Verify(m => m.CreateAsync(It.IsAny<User>()), Times.Never);
            _mockUserRepository.Verify(m => m.GetOneByUsernameAsync(It.IsAny<string>()), Times.Never);
            _mockUserRepository.Verify(m => m.GetOneByEmailAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void GetAllUsers_OK()
        {
            _mockUserRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(UserDatas.listUsers);

            var result = await _userService.GetAllAsync();

            result.Should().BeEquivalentTo(UserDatas.listUsers);

            _mockUserRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void GetOneUserById_Ok()
        {
            int id = 2;

            _mockUserRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(UserDatas.listUsers.ElementAt(id - 1));

            var result = await _userService.GetOneByIdAsync(id);

            result.Should().BeEquivalentTo(UserDatas.listUsers.ElementAt(id - 1));

            _mockUserRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public void GetOneUserById_Bad_NotFound()
        {
            int id = 777;

            _mockUserRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);

            Assert.ThrowsAsync<HttpNotFoundException>(() => _userService.GetOneByIdAsync(id))
                .Equals(ResourceEn.EmailAlreadyExists);

            _mockUserRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        #region Authenticate

        [Fact]
        public async void Login_Ok()
        {
            int userId = 1;

            var userReturned = new User
            {
                Id = UserDatas.listUsers.ElementAt(0).Id,
                Username = UserDatas.listUsers.ElementAt(0).Username,
                Email = UserDatas.listUsers.ElementAt(0).Email,
                Password = UserDatas.listUsers.ElementAt(0).Password,
                Role = UserDatas.listUsers.ElementAt(0).Role
            };

            _mockUserRepository.Setup(m => m.GetOneByUsernameAsync(UserDatas.listUsers.ElementAt(userId -1).Username)).ReturnsAsync(userReturned);

            var result = await _userService.Authenticate(UserDatas.listUsers.ElementAt(userId - 1).Username, UserDatas.listUserPasswords.ElementAt(userId - 1));

            result.Should().BeEquivalentTo(UserDatas.listUsers.ElementAt(userId - 1), opt => opt.Excluding(u => u.Token));
            result.Token.Should().NotBeNull();

            _mockUserRepository.Verify(m => m.GetOneByUsernameAsync(UserDatas.listUsers.ElementAt(userId - 1).Username), Times.Once);
        }

        #endregion
    }
}
