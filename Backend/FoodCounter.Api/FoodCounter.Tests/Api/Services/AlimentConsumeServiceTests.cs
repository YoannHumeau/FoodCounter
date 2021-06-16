using FluentAssertions;
using FoodCounter.Api.Exceptions;
using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Services;
using FoodCounter.Api.Services.Implementations;
using FoodCounter.Tests.ExampleDatas;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async void CreateAliment_Ok()
        {
            _mockAlimentConsumeRepository.Setup(m => m.CreateAsync(AlimentConsumeDatas.newAlimentConsume)).ReturnsAsync(AlimentConsumeDatas.newAlimentConsumeCreated);

            var result = await _alimentConsumeService.CreateAsync(AlimentConsumeDatas.newAlimentConsume);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.newAlimentConsumeCreated);

            _mockAlimentConsumeRepository.Verify(m => m.CreateAsync(AlimentConsumeDatas.newAlimentConsume));
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

            AlimentConsume resultContent;

            Func<Task> result = async () => { resultContent = await _alimentConsumeService.GetOneByIdAsync(id); };

            result.Should().Throw<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentConsumeNotFound);

            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void UpdateAlimentConsume_Ok()
        {
            int id = 2;
            long userId = 3;
            var updateAlimentConsume = new AlimentConsume
            {
                Id = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1).Id,
                Weight = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1).Weight + 111
            };

            var afterUpdateAlimentConsume = new AlimentConsume
            {
                Id = updateAlimentConsume.Id,
                UserId = userId,
                AlimentId = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1).AlimentId,
                Aliment = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1).Aliment,
                ConsumeDate = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1).ConsumeDate,
                Weight = updateAlimentConsume.Weight
            };

            _mockAlimentConsumeRepository.Setup(m => m.UpdateAsync(updateAlimentConsume)).ReturnsAsync(afterUpdateAlimentConsume);

            var result = await _alimentConsumeService.UpdateAsync(updateAlimentConsume);

            result.Should().BeEquivalentTo(afterUpdateAlimentConsume);

            _mockAlimentConsumeRepository.Verify(m => m.UpdateAsync(updateAlimentConsume), Times.Once);
        }

        [Fact]
        public async void UpdateAlimentConsume_Bad_NotFound()
        {
            int id = 2;
            var updateAlimentConsume = new AlimentConsume
            {
                Id = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1).Id,
            };

            _mockAlimentConsumeRepository.Setup(m => m.UpdateAsync(updateAlimentConsume)).ReturnsAsync(() => null);

            var result = await _alimentConsumeService.UpdateAsync(updateAlimentConsume);

            result.Should().BeNull();

            _mockAlimentConsumeRepository.Verify(m => m.UpdateAsync(updateAlimentConsume), Times.Once);
        }

        [Fact]
        public async void DeleteAlimentConsume_Ok()
        {
            int id = 2;

            _mockAlimentConsumeRepository.Setup(m => m.DeleteAsync(id)).ReturnsAsync(true);

            var result = await _alimentConsumeService.DeleteAsync(id);
            result.Should().BeTrue();

            _mockAlimentConsumeRepository.Verify(m => m.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async void DeleteAliment_Bad_NotFound()
        {
            int id = 2;

            _mockAlimentConsumeRepository.Setup(m => m.DeleteAsync(id)).ReturnsAsync(false);

            var result = await _alimentConsumeService.DeleteAsync(id);
            result.Should().BeFalse();

            _mockAlimentConsumeRepository.Verify(m => m.DeleteAsync(id), Times.Once);
        }
    }
}
