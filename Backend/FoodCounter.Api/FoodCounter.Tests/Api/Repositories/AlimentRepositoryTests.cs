using FluentAssertions;
using FoodCounter.Api.Repositories.Implementations;
using Xunit;
using FoodCounter.Tests.ExampleDatas;
using System.Linq;

namespace FoodCounter.Tests.Api.Repositories
{
    public class AlimentRepositoryTests : BaseRepositoryTests
    {
        private readonly AlimentRepository _alimentRepository;

        public AlimentRepositoryTests()
        {
            _alimentRepository = new AlimentRepository(_dbAccess);
        }

        [Fact]
        public async void GetAll_Ok()
        {
            PrepareDatabase();

            var result = await _alimentRepository.GetAllAsync();

            result.Should().BeEquivalentTo(AlimentDatas.listAliments);
        }

        [Fact]
        public async void GetOneById_Ok()
        {
            PrepareDatabase();

            int id = 2;

            var result = await _alimentRepository.GetOneByIdAsync(id);

            result.Should().BeEquivalentTo(AlimentDatas.listAliments.ElementAt(id - 1));
        }

        [Fact]
        public async void GetOneById_Bad_NotFound()
        {
            PrepareDatabase();

            int id = 777;

            var result = await _alimentRepository.GetOneByIdAsync(id);

            result.Should().BeNull();
        }
    }
}
