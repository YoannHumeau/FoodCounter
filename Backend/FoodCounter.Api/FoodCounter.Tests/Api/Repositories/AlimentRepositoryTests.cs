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
        public async void CreateAliment_Ok()
        {
            PrepareDatabase();

            var result = await _alimentRepository.CreateAsync(AlimentDatas.newAliment);

            result.Should().BeEquivalentTo(AlimentDatas.newAlimentDto);

            var resultCheck = await _alimentRepository.GetOneByIdAsync(AlimentDatas.newAlimentDto.Id);
            resultCheck.Should().BeEquivalentTo(AlimentDatas.newAlimentDto);
        }

        [Fact]
        public async void GetAllAliments_Ok()
        {
            PrepareDatabase();

            var result = await _alimentRepository.GetAllAsync();

            result.Should().BeEquivalentTo(AlimentDatas.listAliments);
        }

        [Fact]
        public async void GetOneAlimentById_Ok()
        {
            PrepareDatabase();

            int id = 2;

            var result = await _alimentRepository.GetOneByIdAsync(id);

            result.Should().BeEquivalentTo(AlimentDatas.listAliments.ElementAt(id - 1));
        }

        [Fact]
        public async void GetOneAlimentById_Bad_NotFound()
        {
            PrepareDatabase();

            int id = 777;

            var result = await _alimentRepository.GetOneByIdAsync(id);

            result.Should().BeNull();
        }

        [Fact]
        public async void GetOneAlimentByName_Ok()
        {
            PrepareDatabase();

            var name = AlimentDatas.listAliments.ElementAt(1).Name;

            var result = await _alimentRepository.GetOneByNameAsync(name);

            result.Should().BeEquivalentTo(AlimentDatas.listAliments.ElementAt(1));
        }

        [Fact]
        public async void GetOneByName_Bad_NotFound()
        {
            PrepareDatabase();

            var name = "Troll Name";

            var result = await _alimentRepository.GetOneByNameAsync(name);

            result.Should().BeNull();
        }
    }
}
