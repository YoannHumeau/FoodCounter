using FluentAssertions;
using FoodCounter.Api.Repositories.Implementations;
using FoodCounter.Tests.ExampleDatas;
using Xunit;

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
    }
}
