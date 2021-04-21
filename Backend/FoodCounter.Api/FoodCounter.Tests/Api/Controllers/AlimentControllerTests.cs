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
using AutoMapper;
using FoodCounter.Api.Models;

namespace FoodCounter.Tests.Api.Controllers
{
    public class AlimentControllerTests
    {
        private readonly Mock<ILogger<AlimentController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IAlimentService> _mockAlimentService;
        private readonly AlimentController _alimentController;

        public AlimentControllerTests()
        {
            _mockLogger = new Mock<ILogger<AlimentController>>();
            _mockMapper = new Mock<IMapper>();
            _mockAlimentService = new Mock<IAlimentService>();
            _alimentController = new AlimentController(_mockLogger.Object, _mockMapper.Object, _mockAlimentService.Object);
        }

        [Fact]
        public async void CreateAliment_Ok()
        {
            _mockAlimentService.Setup(m => m.CreateAsync(It.IsAny<AlimentModel>())).ReturnsAsync(AlimentDatas.newAlimentDto);

            var result = await _alimentController.CreateAsync(AlimentDatas.newAlimentCreationDto);
            var okObjectResult = result as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.StatusCode.Should().Be(200);
            okObjectResult.Value.Should().Be(AlimentDatas.newAlimentDto);

            _mockAlimentService.Verify(m => m.CreateAsync(It.IsAny<AlimentModel>()), Times.Once);
        }

        // TODO : Make test for bad json inoyut

        [Fact]
        public async void GetAllAliment_Ok()
        {
            _mockAlimentService.Setup(m => m.GetAllAsync()).ReturnsAsync(AlimentDatas.listAliments);

            var result = await _alimentController.GetAsync(null);
            var okObjectResult = result as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.StatusCode.Should().Be(200);
            okObjectResult.Value.Should().Be(AlimentDatas.listAliments);

            _mockAlimentService.Verify(m => m.GetAllAsync(), Times.Once);
            _mockAlimentService.Verify(m => m.GetOneByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void GetOneAlimentById_Ok()
        {
            int id = 2;
            var aliment = AlimentDatas.listAliments.ElementAt(id - 1);

            _mockAlimentService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(aliment);

            var result = await _alimentController.GetOneByIdAsync(id);
            var okObjectResult = result as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.StatusCode.Should().Be(200);
            okObjectResult.Value.Should().Be(aliment);

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

        [Fact]
        public async void GetOneAlimentByName_Ok()
        {
            int id = 2;
            var aliment = AlimentDatas.listAliments.ElementAt(id - 1);

            _mockAlimentService.Setup(m => m.GetOneByNameAsync(aliment.Name)).ReturnsAsync(aliment);

            var result = await _alimentController.GetAsync(aliment.Name);
            var okObjectResult = result as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.StatusCode.Should().Be(200);
            okObjectResult.Value.Should().Be(aliment);

            _mockAlimentService.Verify(m => m.GetOneByNameAsync(aliment.Name), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentByName_Bad_NotFound()
        {
            var name = "Troll Name";

            _mockAlimentService.Setup(m => m.GetOneByNameAsync(name)).ReturnsAsync(() => null);

            var result = await _alimentController.GetAsync(name);
            var okObjectResult = result as NotFoundObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.StatusCode.Should().Be(404);

            // Put the content as a json and compare
            JsonConvert.SerializeObject(okObjectResult.Value).Should().Be(
                JsonConvert.SerializeObject(new { Message = ResourceEn.AlimentNotFound }));

            _mockAlimentService.Verify(m => m.GetOneByNameAsync(name), Times.Once);
            _mockAlimentService.Verify(m => m.GetAllAsync(), Times.Never);
        }
    }
}
