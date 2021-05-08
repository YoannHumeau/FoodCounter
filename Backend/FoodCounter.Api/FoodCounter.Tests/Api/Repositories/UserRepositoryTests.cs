using FluentAssertions;
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

            var resultCheck = await _userRepository.GetOneByIdAsync(UserDatas.newUserCreated.Id);
            resultCheck.Should().BeEquivalentTo(UserDatas.newUserCreated);
        }

        [Fact]
        public async void GetAllUsers_Ok()
        {
            PrepareDatabase();

            var result = await _userRepository.GetAllAsync();

            result.Should().BeEquivalentTo(UserDatas.listUsers);
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
