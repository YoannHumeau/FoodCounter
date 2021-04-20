using FluentAssertions;
using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Repositories.Implementations;
using FoodCounter.Api.Service;
using FoodCounter.Api.Service.Implementations;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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
            var list = new List<AlimentModel>
            {
                new AlimentModel
                {
                    Id = 1,
                    Name = "Aliment 1",
                    Calories = 200,
                    Barecode = null
                }
            };

            _mockAlimentRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(list);

            var result = await _alimentService.GetAllAsync();

            result.Should().BeEquivalentTo(list);

            _mockAlimentRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}
