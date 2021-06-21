using FluentAssertions;
using Moq;
using FoodCounter.Tests.ExampleDatas;
using Xunit;
using System.Linq;
using FoodCounter.Api.Resources;
using Newtonsoft.Json;
using AutoMapper;
using FoodCounter.Api.Models;
using FoodCounter.Api.Models.Dto;
using FoodCounter.Api.Controllers;
using Microsoft.Extensions.Logging;
using FoodCounter.Api.Services;
using Microsoft.AspNetCore.Mvc;
using FoodCounter.Api.Entities;
using System;
using FoodCounter.Api.Exceptions;
using System.Threading.Tasks;

namespace FoodCounter.Tests.Api.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _mockLogger = new Mock<ILogger<UserController>>();
            _mockUserService = new Mock<IUserService>();

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(typeof(AutoMapping))));

            _userController = new UserController(_mockLogger.Object, mapper, _mockUserService.Object);
        }

        [Fact]
        public async void CreateUser_Ok()
        {
            _mockUserService.Setup(m => m.CreateAsync(It.IsAny<User>())).ReturnsAsync(UserDatas.newUserCreated);

            var result = await _userController.CreateAsync(UserDatas.newUserCreationModelDto);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().Be(UserDatas.newUserCreated);

            _mockUserService.Verify(m => m.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void CreateUser_Bad_UsernameAlreadyExists()
        {
            var badNewUser = new UserCreationDto
            {
                Username = UserDatas.listUsers.ElementAt(2).Username,
                Email = "new@email.tld",
                Password = "123456"
            };

            _mockUserService.Setup(m => m.CreateAsync(It.IsAny<User>())).ThrowsAsync(new HttpConflictException(ResourceEn.UsernameAlreadyExists));

            IActionResult resultContent;

            Func<Task> result = async () => { resultContent = await _userController.CreateAsync(badNewUser); };
            result.Should()
                .Throw<HttpConflictException>()
                .WithMessage(ResourceEn.UsernameAlreadyExists);

            _mockUserService.Verify(m => m.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void CreateUser_Bad_EmailAlreadyExists()
        {
            var badNewUser = new UserCreationDto
            {
                Username = "newusername",
                Email = UserDatas.listUsers.ElementAt(2).Email,
                Password = "123456"
            };

            _mockUserService.Setup(m => m.CreateAsync(It.IsAny<User>())).ThrowsAsync(new HttpConflictException(ResourceEn.EmailAlreadyExists));

            IActionResult resultContent;

            Func<Task> result = async () => { resultContent = await _userController.CreateAsync(badNewUser); };
            result.Should()
                .Throw<HttpConflictException>()
                .WithMessage(ResourceEn.EmailAlreadyExists);

            _mockUserService.Verify(m => m.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void GetAllUser_Ok()
        {
            _mockUserService.Setup(m => m.GetAllAsync()).ReturnsAsync(UserDatas.listUsers);

            var result = await _userController.GetAsync();
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().Be(UserDatas.listUsers);

            _mockUserService.Verify(m => m.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void GetOneUserById_Ok()
        {
            int id = 2;
            var user = UserDatas.listUsers.ElementAt(id - 1);

            _mockUserService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(user);

            var result = await _userController.GetOneByIdAsync(id);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().Be(user);

            _mockUserService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void GetOneUserById_Bad_NotFound()
        {
            int id = 777;

            _mockUserService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);

            var result = await _userController.GetOneByIdAsync(id);
            var objectResult = result as NotFoundObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(404);

            // Put the content as a json and compare
            JsonConvert.SerializeObject(objectResult.Value).Should().Be(
                JsonConvert.SerializeObject(new { Message = ResourceEn.UserNotFound }));

            _mockUserService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }
    }
}
