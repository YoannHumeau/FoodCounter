using AutoMapper;
using FluentAssertions;
using FoodCounter.Api.Controllers;
using FoodCounter.Api.Models;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Service;
using FoodCounter.Tests.ExampleDatas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FoodCounter.Tests.Api.Controllers
{
    public class AlimentConsumeControllerTests
    {
        private readonly Mock<ILogger<AlimentConsumeController>> _mockLogger;
        private readonly Mock<IAlimentConsumeService> _mockAlimentConsumeService;
        private readonly AlimentConsumeController _alimentConsumeController;

        public AlimentConsumeControllerTests()
        {
            _mockLogger = new Mock<ILogger<AlimentConsumeController>>();
            _mockAlimentConsumeService = new Mock<IAlimentConsumeService>();

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(typeof(AutoMapping))));

            _alimentConsumeController = new AlimentConsumeController(_mockLogger.Object, mapper, _mockAlimentConsumeService.Object);
        }

        [Fact]
        public async void GetOneAlimentById_Ok()
        {
            int id = 2;
            var aliment = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1);

            _mockAlimentConsumeService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(aliment);

            var result = await _alimentConsumeController.GetOneByIdAsync(id);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().Be(aliment);

            _mockAlimentConsumeService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentById_Bad_NotFound()
        {
            int id = 777;

            _mockAlimentConsumeService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);

            var result = await _alimentConsumeController.GetOneByIdAsync(id);
            var objectResult = result as NotFoundObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(404);

            // Put the content as a json and compare
            JsonConvert.SerializeObject(objectResult.Value).Should().Be(
                JsonConvert.SerializeObject(new { Message = ResourceEn.AlimentNotFound }));

            _mockAlimentConsumeService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }
    }
}
