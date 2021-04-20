using FluentAssertions;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Service;
using FoodCounter.Api.Service.Implementations;
using Moq;
using Xunit;
using FoodCounter.Tests.ExampleDatas;
using System.Linq;

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
    }
}
