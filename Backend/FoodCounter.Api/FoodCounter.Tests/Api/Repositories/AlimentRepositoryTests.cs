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

        [Fact]
        public async void DeletedOneById_Ok()
        {
            PrepareDatabase();

            int id = 2;

            // Check the aliment exists
            var resultBefore = await _alimentRepository.GetOneByIdAsync(id);
            resultBefore.Should().BeEquivalentTo(AlimentDatas.listAliments.ElementAt(id - 1));

            // Check that another aliment exists
            var resultOtherBefore = await _alimentRepository.GetOneByIdAsync(id + 1);
            resultOtherBefore.Should().BeEquivalentTo(AlimentDatas.listAliments.ElementAt(id));

            // Delete the aliment
            var result = await _alimentRepository.DeleteAsync(id);
            result.Should().Be(true);

            // Check the aliment is deleted
            var resultAfter = await _alimentRepository.GetOneByIdAsync(id);
            resultAfter.Should().BeNull();

            // Check that the other aliment still exists
            var resultOtherAfter = await _alimentRepository.GetOneByIdAsync(id + 1);
            resultOtherAfter.Should().BeEquivalentTo(AlimentDatas.listAliments.ElementAt(id));
        }

        [Fact]
        public async void DeletedOneById_Bad_NotFound()
        {
            PrepareDatabase();

            int id = 777;
            int otherId = 3;

            // Check the aliment does not exists
            var resultBefore = await _alimentRepository.GetOneByIdAsync(id);
            resultBefore.Should().BeNull();

            // Check that another aliment exists
            var resultOtherBefore = await _alimentRepository.GetOneByIdAsync(otherId + 1);
            resultOtherBefore.Should().BeEquivalentTo(AlimentDatas.listAliments.ElementAt(otherId));

            // Try to delete 
            var result = await _alimentRepository.DeleteAsync(id);
            result.Should().Be(false);

            // Check that the other aliment still exists
            var resultOtherAfter = await _alimentRepository.GetOneByIdAsync(otherId + 1);
            resultOtherAfter.Should().BeEquivalentTo(AlimentDatas.listAliments.ElementAt(otherId));
        }
    }
}
