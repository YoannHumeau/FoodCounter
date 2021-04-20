using FluentAssertions;
using FoodCounter.Api.Controllers;
using FoodCounter.Api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FoodCounter.Tests.ExampleDatas;
using Xunit;
using System.Linq;
using FoodCounter.Api.Resources;
using Newtonsoft.Json;

namespace FoodCounter.Tests.Api.Controllers
{
    public class AlimentControllerTests
    {
        private readonly Mock<ILogger<AlimentController>> _mockIlogger;
        private readonly Mock<IAlimentService> _mockAlimentService;
        private readonly AlimentController _alimentController;

        public AlimentControllerTests()
        {
            _mockAlimentService = new Mock<IAlimentService>();
            _mockIlogger = new Mock<ILogger<AlimentController>>();
            _alimentController = new AlimentController(_mockIlogger.Object, _mockAlimentService.Object);
        }

        [Fact]
        public async void GetAll_Ok()
        {
            _mockAlimentService.Setup(m => m.GetAllAsync()).ReturnsAsync(AlimentDatas.listAliments);

            var result = await _alimentController.GetAllAsync();
            var okObjectResult = result as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.StatusCode.Should().Be(200);
            okObjectResult.Value.Should().Be(AlimentDatas.listAliments);

            _mockAlimentService.Verify(m => m.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentById_Ok()
        {
            int id = 2;

            _mockAlimentService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(AlimentDatas.listAliments.ElementAt(id - 1));

            var result = await _alimentController.GetOneByIdAsync(id);
            var okObjectResult = result as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.StatusCode.Should().Be(200);
            okObjectResult.Value.Should().Be(AlimentDatas.listAliments.ElementAt(id - 1));

            _mockAlimentService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentById_Bad_NotFound()
        {
            int id = 777;

            _mockAlimentService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);

            var result = await _alimentController.GetOneByIdAsync(id);
            var okObjectResult = result as NotFoundObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.StatusCode.Should().Be(404);

            // Put the content as a json and compare
            JsonConvert.SerializeObject(okObjectResult.Value).Should().Be(
                JsonConvert.SerializeObject( new { Message = ResourceEn.AlimentNotFound } ));

            _mockAlimentService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }
    }
}
