using AutoMapper;
using FluentAssertions;
using FoodCounter.Api.Controllers;
using FoodCounter.Api.Exceptions;
using FoodCounter.Api.Models;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Services;
using FoodCounter.Tests.ExampleDatas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        public async void GetOneAlimentConsumeById_Ok()
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
        public async void GetOneAlimentConsumeByIdFOrAnotherUserWithAdmin_Ok()
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
        public void GetOneAlimentConsumeById_Bad_Forbidden_BadUser()
        {
            MockUser(3); // Simple user (Benjamin)

            int id = 5;
            IActionResult resultContent = null;

            _mockAlimentConsumeService.Setup(m => m.GetOneByIdAsync(id)).ThrowsAsync(new HttpForbiddenException(ResourceEn.AccessDenied));

            Func<Task> result = async () => { resultContent = await _alimentConsumeController.GetOneByIdAsync(id); };
            result.Should()
                .Throw<HttpForbiddenException>()
                .WithMessage(ResourceEn.AccessDenied);

            _mockAlimentConsumeService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public void GetOneAlimentConsumeById_Bad_NotFound()
        {
            int id = 777;
            IActionResult resultContent = null;

            _mockAlimentConsumeService.Setup(m => m.GetOneByIdAsync(id)).ThrowsAsync(new HttpNotFoundException(ResourceEn.AlimentConsumeNotFound));

            Func<Task> result = async () => { resultContent = await _alimentConsumeController.GetOneByIdAsync(id); };
            result.Should()
                .Throw<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentConsumeNotFound);

            _mockAlimentConsumeService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void UpdateAlimentConsume_Ok()
        {
            MockUser(3); // Simple user (Benjamin)

            var alimentConsumeId = AlimentConsumeDatas.afterUpdateAlimentConsume.Id;

            _mockAlimentConsumeService.Setup(m => m.UpdateAsync(It.IsAny<AlimentConsume>())).ReturnsAsync(AlimentConsumeDatas.afterUpdateAlimentConsume);

            var result = await _alimentConsumeController.UpdateAsync(alimentConsumeId, AlimentConsumeDatas.updateAlimentConsumeUpdateDto);
            var objectResult = result as OkObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(AlimentConsumeDatas.afterUpdateAlimentConsumeDto);

            _mockAlimentConsumeService.Verify(m => m.UpdateAsync(It.IsAny<AlimentConsume>()), Times.Once);
        }

        [Fact]
        public void UpdateAlimentConsume_Bad_NotFound()
        {
            int id = 777;
            IActionResult resultContent = null;

            _mockAlimentConsumeService.Setup(m => m.UpdateAsync(It.IsAny<AlimentConsume>())).ThrowsAsync(new HttpNotFoundException(ResourceEn.AlimentConsumeNotFound));

            Func<Task> result = async () => { resultContent = await _alimentConsumeController.UpdateAsync(id, AlimentConsumeDatas.updateAlimentConsumeUpdateDto); };
            result.Should()
                .Throw<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentConsumeNotFound);

            _mockAlimentConsumeService.Verify(m => m.UpdateAsync(It.IsAny<AlimentConsume>()), Times.Once);
        }

        [Fact]
        public async void DeleteOneAlimentConsumeById_Ok()
        {
            MockUser(3); // Simple user (Benjamin)

            int id = 2;

            _mockAlimentConsumeService.Setup(m => m.DeleteAsync(id)).ReturnsAsync(true);

            var result = await _alimentConsumeController.DeleteAsync(id);
            var objectResult = result as NoContentResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(204);

            _mockAlimentConsumeService.Verify(m => m.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async void DeleteOneAlimentConsumeByIdForAnotherUserWithAdmin_Ok()
        {
            MockUser(1); // Admin user (Wayne)

            int id = 2;

            _mockAlimentConsumeService.Setup(m => m.DeleteAsync(id)).ReturnsAsync(true);

            var result = await _alimentConsumeController.DeleteAsync(id);
            var objectResult = result as NoContentResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(204);

            _mockAlimentConsumeService.Verify(m => m.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public void DeleteOneAlimentConsumeById_Bad_NotFound()
        {
            int id = 777;
            IActionResult resultContent = null;

            _mockAlimentConsumeService.Setup(m => m.GetOneByIdAsync(id)).ThrowsAsync(new HttpNotFoundException(ResourceEn.AlimentConsumeNotFound));

            Func<Task> result = async () => { resultContent = await _alimentConsumeController.GetOneByIdAsync(id); };
            result.Should()
                .Throw<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentConsumeNotFound);

            _mockAlimentConsumeService.Verify(m => m.GetOneByIdAsync(id), Times.Once);
            _mockAlimentConsumeService.Verify(m => m.DeleteAsync(id), Times.Never);
        }

        [Fact]
        public async void DeleteOneAlimentConsumeById_Bad_InternalServerError()
        {
            MockUser(3); // Simple user (Benjamin)

            int id = 2;

            _mockAlimentConsumeService.Setup(m => m.DeleteAsync(id)).ReturnsAsync(false);

            var result = await _alimentConsumeController.DeleteAsync(id);
            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(500);

            // Put the content as a json and compare
            JsonConvert.SerializeObject(objectResult.Value).Should().Be(
                JsonConvert.SerializeObject(new { Message = ResourceEn.ProblemDeleting }));

            _mockAlimentConsumeService.Verify(m => m.DeleteAsync(id), Times.Once);
        }
    }
}
