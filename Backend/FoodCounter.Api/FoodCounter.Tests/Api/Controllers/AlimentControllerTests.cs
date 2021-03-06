using FluentAssertions;
using FoodCounter.Api.Controllers;
using FoodCounter.Api.Services;
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
using FoodCounter.Api.Models.Dto;
using FoodCounter.Api.Exceptions;
using System;
using System.Threading.Tasks;

namespace FoodCounter.Tests.Api.Controllers
{
    public class AlimentControllerTests
    {
        private readonly Mock<ILogger<AlimentController>> _mockLogger;
        private readonly Mock<IAlimentService> _mockAlimentService;
        private readonly AlimentController _alimentController;

        public AlimentControllerTests()
        {
            _mockLogger = new Mock<ILogger<AlimentController>>();
            _mockAlimentService = new Mock<IAlimentService>();

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(typeof(AutoMapping))));

            _alimentController = new AlimentController(_mockLogger.Object, mapper, _mockAlimentService.Object);
        }

        [Fact]
        public async void CreateAliment_Ok()
        {
            _mockAlimentService.Setup(m => m.CreateAsync(It.IsAny<Aliment>())).ReturnsAsync(AlimentDatas.newAlimentDto);

            var result = await _alimentController.CreateAsync(AlimentDatas.newAlimentCreationDto);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().Be(AlimentDatas.newAlimentDto);

            _mockAlimentService.Verify(m => m.CreateAsync(It.IsAny<Aliment>()), Times.Once);
        }

        [Fact]
        public async void GetAllAliment_Ok()
        {
            _mockAlimentService.Setup(m => m.GetAllAsync()).ReturnsAsync(AlimentDatas.listAliments);

            var result = await _alimentController.GetAsync(null);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(AlimentDatas.listAlimentsDto);

            _mockAlimentService.Verify(m => m.GetAllAsync(), Times.Once);
            _mockAlimentService.Verify(m => m.GetOneByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void GetOneAlimentById_Ok()
        {
            int id = 2;
            var aliment = AlimentDatas.listAliments.ElementAt(id - 1);
            var alimentDto = AlimentDatas.listAlimentsDto.ElementAt(id - 1);

            _mockAlimentService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(aliment);

            var result = await _alimentController.GetOneByIdAsync(id);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(alimentDto);

            _mockAlimentService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public void GetOneAlimentById_Bad_NotFound()
        {
            int id = 777;
            IActionResult resultContent = null;

            _mockAlimentService.Setup(m => m.GetOneByIdAsync(id)).ThrowsAsync(new HttpNotFoundException(ResourceEn.AlimentNotFound));

            Func<Task> result = async () => { resultContent = await _alimentController.GetOneByIdAsync(id); };
            result.Should()
                .ThrowAsync<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentNotFound);

            _mockAlimentService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentByName_Ok()
        {
            int id = 2;
            var aliment = AlimentDatas.listAliments.ElementAt(id - 1);
            var alimentDto = AlimentDatas.listAlimentsDto.ElementAt(id - 1);

            _mockAlimentService.Setup(m => m.GetOneByNameAsync(aliment.Name)).ReturnsAsync(aliment);

            var result = await _alimentController.GetAsync(aliment.Name);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(alimentDto);

            _mockAlimentService.Verify(m => m.GetOneByNameAsync(aliment.Name), Times.Once);
        }

        [Fact]
        public void GetOneAlimentByName_Bad_NotFound()
        {
            var name = "Troll Name";
            IActionResult resultContent = null;

            _mockAlimentService.Setup(m => m.GetOneByNameAsync(name)).ThrowsAsync(new HttpNotFoundException(ResourceEn.AlimentNotFound));

            Func<Task> result = async () => { resultContent = await _alimentController.GetAsync(name); };
            result.Should()
                .ThrowAsync<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentNotFound);

            _mockAlimentService.Verify(m => m.GetOneByNameAsync(name), Times.Once);
            _mockAlimentService.Verify(m => m.GetAllAsync(), Times.Never);
        }

        [Fact]
        public async void DeleteOneAlimentById_Ok()
        {
            int id = 2;

            _mockAlimentService.Setup(m => m.DeleteAsync(id)).ReturnsAsync(true);

            var result = await _alimentController.DeleteAsync(id);
            var objectResult = result as NoContentResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(204);

            _mockAlimentService.Verify(m => m.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public void DeleteOneAlimentById_Bad_NotFound()
        {
            int id = 777;
            IActionResult resultContent = null;

            _mockAlimentService.Setup(m => m.DeleteAsync(id)).ThrowsAsync(new HttpNotFoundException(ResourceEn.AlimentNotFound));

            Func<Task> result = async () => { resultContent = await _alimentController.DeleteAsync(id); };
            result.Should()
                .ThrowAsync<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentNotFound);

            _mockAlimentService.Verify(m => m.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async void UpdateAliment_Ok()
        {
            _mockAlimentService.Setup(m => m.UpdateAsync(It.IsAny<Aliment>())).ReturnsAsync(AlimentDatas.updateAliment);

            var result = await _alimentController.UpdateAsync(AlimentDatas.updateAliment.Id, AlimentDatas.updateAlimentUpdateDto);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(AlimentDatas.updatedAlimentDto);

            _mockAlimentService.Verify(m => m.UpdateAsync(It.IsAny<Aliment>()), Times.Once);
        }

        [Fact]
        public void UpdateAliment_Bad_NotFound()
        {
            long id = 777;
            IActionResult resultContent = null;

            _mockAlimentService.Setup(m => m.UpdateAsync(It.IsAny<Aliment>())).ThrowsAsync(new HttpNotFoundException(ResourceEn.AlimentNotFound));

            Func<Task> result = async () => { resultContent = await _alimentController.UpdateAsync(id, AlimentDatas.updateAlimentUpdateDto); };
            result.Should()
                .ThrowAsync<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentNotFound);

            _mockAlimentService.Verify(m => m.UpdateAsync(It.IsAny<Aliment>()), Times.Once);
        }
    }
}
