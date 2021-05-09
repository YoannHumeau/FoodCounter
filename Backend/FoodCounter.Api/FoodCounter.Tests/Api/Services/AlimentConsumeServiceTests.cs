using FluentAssertions;
using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Service;
using FoodCounter.Api.Service.Implementations;
using FoodCounter.Tests.ExampleDatas;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FoodCounter.Tests.Api.Services
{
    public class AlimentConsumeServiceTests
    {
        private readonly IAlimentConsumeService _alimentConsumeService;
        private readonly Mock<IAlimentConsumeRepository> _mockAlimentConsumeRepository;

        public AlimentConsumeServiceTests()
        {
            _mockAlimentConsumeRepository = new Mock<IAlimentConsumeRepository>();
            _alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object);
        }

        [Fact]
        public async void GetAllAlimentsByUserId_OK()
        {
            long userId = 3;

            _mockAlimentConsumeRepository.Setup(m => m.GetAllByUserIdAsync(userId)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.Where(x => x.UserId == userId));

            var result = await _alimentConsumeService.GetAllByUserIdAsync(userId);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.listAlimentConsumes.Where(x => x.UserId == userId));

            _mockAlimentConsumeRepository.Verify(x => x.GetAllByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async void GetAllAlimentsByUserId_Bad_ResultEmpty()
        {
            long userId = 2;

            _mockAlimentConsumeRepository.Setup(m => m.GetAllByUserIdAsync(userId)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.Where(x => x.UserId == userId));

            var result = await _alimentConsumeService.GetAllByUserIdAsync(userId);

            result.Should().BeEquivalentTo(new List<AlimentConsume>());

            _mockAlimentConsumeRepository.Verify(x => x.GetAllByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentById_Ok()
        {
            int id = 2;

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));

            var result = await _alimentConsumeService.GetOneByIdAsync(id);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));

            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentById_Bad_NotFound()
        {
            int id = 777;

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);

            var result = await _alimentConsumeService.GetOneByIdAsync(id);

            result.Should().BeNull();

            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }
    }
}
