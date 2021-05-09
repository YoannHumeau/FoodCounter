using AutoMapper;
using FluentAssertions;
using FoodCounter.Api.Controllers;
using FoodCounter.Api.Models;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Service;
using FoodCounter.Tests.ExampleDatas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
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

        /// <summary>
        /// Used to mock a user logged
        /// </summary>
        /// <param name="userId"></param>
        private void MockUser(long userId)
        {
            var role = UserDatas.listUsers.ElementAt((int)userId - 1).Role;

            _alimentConsumeController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userId.ToString()),
                        new Claim(ClaimTypes.Role, role.ToString())
                    }))        
                }
            };
        }

        [Fact]
        public async void CreateAlimentConsume_Ok()
        {
            MockUser(3); // Simple user (Benjamin)

            _mockAlimentConsumeService.Setup(m => m.CreateAsync(It.IsAny<AlimentConsume>())).ReturnsAsync(AlimentConsumeDatas.newAlimentConsumeCreated);

            var result = await _alimentConsumeController.CreateAsync(AlimentConsumeDatas.newAlimentConsumeCreationDto);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(AlimentConsumeDatas.newAlimentConsumeCreatedDto);

            _mockAlimentConsumeService.Verify(m => m.CreateAsync(It.IsAny<AlimentConsume>()), Times.Once);
        }

        // TODO : Check for failed creation Test 

        [Fact]
        public async void GetOneAlimentById_Ok()
        {
            MockUser(3); // Simple user (Benjamin)

            int id = 2;
            var alimentConsume = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1);
            var alimentConsumeDto = AlimentConsumeDatas.listAlimentConsumesDto.ElementAt(id - 1);

            _mockAlimentConsumeService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(alimentConsume);

            var result = await _alimentConsumeController.GetOneByIdAsync(id);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(alimentConsumeDto);

            _mockAlimentConsumeService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentByIdFOrAnotherUserWithAdmin_Ok()
        {
            MockUser(1); // Admin user (Wayne)

            int id = 2;
            var alimentConsume = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1);
            var alimentConsumeDto = AlimentConsumeDatas.listAlimentConsumesDto.ElementAt(id - 1);

            _mockAlimentConsumeService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(alimentConsume);

            var result = await _alimentConsumeController.GetOneByIdAsync(id);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(alimentConsumeDto);

            _mockAlimentConsumeService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentById_Bad_Forbidden_BadUser()
        {
            MockUser(3); // Simple user (Benjamin)

            int id = 5;
            var alimentConsume = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1);

            _mockAlimentConsumeService.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(alimentConsume);

            var result = await _alimentConsumeController.GetOneByIdAsync(id);
            var objectResult = result as ForbidResult;

            objectResult.Should().NotBeNull();

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
                JsonConvert.SerializeObject(new { Message = ResourceEn.AlimentConsumeNotFound }));

            _mockAlimentConsumeService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }
    }
}
