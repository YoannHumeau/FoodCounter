using FluentAssertions;
using FoodCounter.Api.Repositories.Implementations;
using FoodCounter.Tests.ExampleDatas;
using Xunit;
using System.Linq;
using FoodCounter.Api.Entities;

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
            resultCheck.Should().BeEquivalentTo(UserDatas.newUserCreated, opt => opt.Excluding(x => x.Password));
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

        [Fact]
        public async void GetOneUserByUsername_Ok()
        {
            PrepareDatabase();

            var userId = 3; // user Benjamin
            var user = UserDatas.listUsers.ElementAt(userId - 1);

            var result = await _userRepository.GetOneByUsernameAsync(user.Username);

            result.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async void GetOneUserByUsername_Bad_NotFound()
        {
            PrepareDatabase();

            string username = "TrollUsername";

            var result = await _userRepository.GetOneByUsernameAsync(username);

            result.Should().BeNull();
        }

        [Fact]
        public async void GetOneUserByEmail_Ok()
        {
            PrepareDatabase();

            var userId = 3; // user Benjamin
            var user = UserDatas.listUsers.ElementAt(userId - 1);

            var result = await _userRepository.GetOneByEmailAsync(user.Email);

            result.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async void GetOneUserByEmail_Bad_NotFound()
        {
            PrepareDatabase();

            string email = "troll@email.tld";

            var result = await _userRepository.GetOneByEmailAsync(email);

            result.Should().BeNull();
        }
    }
}
