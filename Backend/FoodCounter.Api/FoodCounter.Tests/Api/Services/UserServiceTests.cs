using FluentAssertions;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Service;
using Moq;
using Xunit;
using System.Linq;
using FoodCounter.Tests.ExampleDatas;

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
        public async void CreateUser_Ok()
        {
            _mockUserRepository.Setup(m => m.CreateAsync(UserDatas.newUser)).ReturnsAsync(UserDatas.newUserCreated);

            var result = await _userService.CreateAsync(UserDatas.newUser);

            result.Should().BeEquivalentTo(UserDatas.newUserCreated);

            _mockUserRepository.Verify(m => m.CreateAsync(UserDatas.newUser));
        }

        // TODO : Tests for fail user creation (already exists)

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
        public async void GetOneUserById_Bad_NotFound()
        {
            int id = 777;

            _mockUserRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);

            var result = await _userService.GetOneByIdAsync(id);

            result.Should().BeNull();

            _mockUserRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }
    }
}
