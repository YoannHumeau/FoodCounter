using FluentAssertions;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Services;
using FoodCounter.Api.Services.Implementations;
using Moq;
using Xunit;
using FoodCounter.Tests.ExampleDatas;
using System.Linq;
using FoodCounter.Api.Models;

namespace FoodCounter.Tests.Api.Services
{
    public class AlimentServiceTests
    {
        private readonly IAlimentService _alimentService;
        private readonly Mock<IAlimentRepository> _mockAlimentRepository;

        public AlimentServiceTests()
        {
            _mockAlimentRepository = new Mock<IAlimentRepository>();
            _alimentService = new AlimentService(_mockAlimentRepository.Object);
        }

        [Fact]
        public async void CreateAliment_Ok()
        {
            var newAliment = new Aliment
            {
                Id = 8,
                Name = AlimentDatas.newAliment.Name,
                Calories = AlimentDatas.newAliment.Calories,
                Barecode = AlimentDatas.newAliment.Barecode
            };

            _mockAlimentRepository.Setup(m => m.CreateAsync(AlimentDatas.newAliment)).ReturnsAsync(newAliment);

            var result = await _alimentService.CreateAsync(AlimentDatas.newAliment);

            result.Should().BeEquivalentTo(newAliment);

            _mockAlimentRepository.Verify(m => m.CreateAsync(AlimentDatas.newAliment));
        }

        [Fact]
        public async void GetAllAliments_OK()
        {
            _mockAlimentRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(AlimentDatas.listAliments);

            var result = await _alimentService.GetAllAsync();

            result.Should().BeEquivalentTo(AlimentDatas.listAliments);

            _mockAlimentRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentById_Ok()
        {
            int id = 2;

            _mockAlimentRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(AlimentDatas.listAliments.ElementAt(id - 1));

            var result = await _alimentService.GetOneByIdAsync(id);

            result.Should().BeEquivalentTo(AlimentDatas.listAliments.ElementAt(id - 1));

            _mockAlimentRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentById_Bad_NotFound()
        {
            int id = 777;

            _mockAlimentRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);

            var result = await _alimentService.GetOneByIdAsync(id);

            result.Should().BeNull();

            _mockAlimentRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentByName_Ok()
        {
            int id = 1;
            var name = AlimentDatas.listAliments.ElementAt(id).Name;

            _mockAlimentRepository.Setup(m => m.GetOneByNameAsync(name)).ReturnsAsync(AlimentDatas.listAliments.ElementAt(id));

            var result = await _alimentService.GetOneByNameAsync(name);

            result.Should().BeEquivalentTo(AlimentDatas.listAliments.ElementAt(id));

            _mockAlimentRepository.Verify(x => x.GetOneByNameAsync(name), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentByName_Bad_NotFound()
        {
            string name = "Troll Name";

            _mockAlimentRepository.Setup(m => m.GetOneByNameAsync(name)).ReturnsAsync(() => null);

            var result = await _alimentService.GetOneByNameAsync(name);

            result.Should().BeNull();

            _mockAlimentRepository.Verify(x => x.GetOneByNameAsync(name), Times.Once);
        }

        [Fact]
        public async void UpdateAliment_Ok()
        {
            var updateAliment = AlimentDatas.updateAliment;

            _mockAlimentRepository.Setup(m => m.UpdateAsync(updateAliment)).ReturnsAsync(updateAliment);

            var result = await _alimentService.UpdateAsync(updateAliment);

            result.Should().BeEquivalentTo(updateAliment);

            _mockAlimentRepository.Verify(m => m.UpdateAsync(updateAliment), Times.Once);
        }

        [Fact]
        public async void UpdateAliment_Bad_NotFound()
        {
            var updateAliment = AlimentDatas.updateAliment;

            _mockAlimentRepository.Setup(m => m.UpdateAsync(updateAliment)).ReturnsAsync(() => null);

            var result = await _alimentService.UpdateAsync(updateAliment);

            result.Should().BeNull();

            _mockAlimentRepository.Verify(m => m.UpdateAsync(updateAliment), Times.Once);
        }

        [Fact]
        public async void DeleteAliment_Ok()
        {
            int id = 2;

            _mockAlimentRepository.Setup(m => m.DeleteAsync(id)).ReturnsAsync(true);

            var result = await _alimentService.DeleteAsync(id);
            result.Should().BeTrue();

            _mockAlimentRepository.Verify(m => m.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async void DeleteAliment_Bad_NotFound()
        {
            int id = 2;

            _mockAlimentRepository.Setup(m => m.DeleteAsync(id)).ReturnsAsync(false);

            var result = await _alimentService.DeleteAsync(id);
            result.Should().BeFalse();

            _mockAlimentRepository.Verify(m => m.DeleteAsync(id), Times.Once);
        }
    }
}
