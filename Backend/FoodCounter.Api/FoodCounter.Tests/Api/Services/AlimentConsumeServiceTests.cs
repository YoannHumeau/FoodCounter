using FluentAssertions;
using FoodCounter.Api.Exceptions;
using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Resources;
using FoodCounter.Api.Services;
using FoodCounter.Api.Services.Implementations;
using FoodCounter.Tests.ExampleDatas;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace FoodCounter.Tests.Api.Services
{
    public class AlimentConsumeServiceTests
    {
        private readonly IAlimentConsumeService _alimentConsumeService;
        private readonly Mock<IAlimentConsumeRepository> _mockAlimentConsumeRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public AlimentConsumeServiceTests()
        {
            _mockAlimentConsumeRepository = new Mock<IAlimentConsumeRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            #region Dirty code to clean
            //var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            //{
            //    new Claim(ClaimTypes.Name, "example name"),
            //    new Claim(ClaimTypes.NameIdentifier, "1"),
            //    new Claim("custom-claim", "example claim value"),
            //}, "mock"));

            //var wow = new DefaultHttpContext() { User = user };

            //_mockHttpContextAccessor.Setup(req => req.HttpContext).Returns(wow);
            #endregion

            // TODO : Find a way to mock and override in methods the mock for a different user

            _alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);
        }

        //private void MockUser(long userId)
        //{
        //    var role = UserDatas.listUsers.ElementAt((int)userId - 1).Role;

        //    _alimentConsumeController.ControllerContext = new ControllerContext
        //    {
        //        HttpContext = new DefaultHttpContext
        //        {
        //            User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        //            {
        //                new Claim(ClaimTypes.Name, userId.ToString()),
        //                new Claim(ClaimTypes.Role, role.ToString())
        //            }))
        //        }
        //    };
        //}

        private void MockUser(long userId)
        {
            var role = UserDatas.listUsers.ElementAt((int)userId - 1).Role;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
            {
                new Claim(ClaimTypes.Name, userId.ToString()),
                new Claim(ClaimTypes.Role, role.ToString()) 
            }));

            _mockHttpContextAccessor.Setup(req => req.HttpContext).Returns(new DefaultHttpContext() { User = user });
        }


        [Fact]
        public async void CreateAlimentConsume_Ok()
        {
            _mockAlimentConsumeRepository.Setup(m => m.CreateAsync(AlimentConsumeDatas.newAlimentConsume)).ReturnsAsync(AlimentConsumeDatas.newAlimentConsumeCreated);

            var result = await _alimentConsumeService.CreateAsync(AlimentConsumeDatas.newAlimentConsume);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.newAlimentConsumeCreated);

            _mockAlimentConsumeRepository.Verify(m => m.CreateAsync(AlimentConsumeDatas.newAlimentConsume));
        }

        [Fact]
        public async void GetAllAlimentsConsumeByUserId_OK()
        {
            MockUser(3); // Simple user (Benjamin)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            long userId = 3;

            _mockAlimentConsumeRepository.Setup(m => m.GetAllByUserIdAsync(userId)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.Where(x => x.UserId == userId));

            var result = await alimentConsumeService.GetAllByUserIdAsync(userId);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.listAlimentConsumes.Where(x => x.UserId == userId));

            _mockAlimentConsumeRepository.Verify(x => x.GetAllByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async void GetAllAlimentConsumesByUserId_Bad_ResultEmpty()
        {
            MockUser(4); // Simple user (Cassandra)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            long userId = 4;

            _mockAlimentConsumeRepository.Setup(m => m.GetAllByUserIdAsync(userId)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.Where(x => x.UserId == userId));

            var result = await alimentConsumeService.GetAllByUserIdAsync(userId);

            result.Should().BeEquivalentTo(new List<AlimentConsume>());

            _mockAlimentConsumeRepository.Verify(x => x.GetAllByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public void GetAllAlimentConsumeByUserId_Bad_Forbidden()
        {
            MockUser(4); // Simple user (Cassandra)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            long userId = 3;

            IEnumerable<AlimentConsume> resultContent;

            Func<Task> result = async () => { resultContent = await alimentConsumeService.GetAllByUserIdAsync(userId); };

            result.Should()
                .Throw<HttpForbiddenException>()
                .WithMessage(ResourceEn.AccessDenied);

            _mockAlimentConsumeRepository.Verify(x => x.GetAllByUserIdAsync(userId), Times.Never);
        }

        [Fact]
        public async void GetAllAlimentConsumeByUserId_Ok_AdminCanPass()
        {
            MockUser(1); // Admin user (Wayne)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            long userId = 3;

            _mockAlimentConsumeRepository.Setup(m => m.GetAllByUserIdAsync(userId)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.Where(x => x.UserId == userId));

            var result = await alimentConsumeService.GetAllByUserIdAsync(userId);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.listAlimentConsumes.Where(x => x.UserId == userId));

            _mockAlimentConsumeRepository.Verify(x => x.GetAllByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async void GetOneAlimentConsumeById_Ok()
        {
            MockUser(3); // Simple user (Benjamin)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            int id = 2;

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));

            var result = await alimentConsumeService.GetOneByIdAsync(id);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));

            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public void GetOneAlimentConsumeById_Bad_Forbidden()
        {
            MockUser(4); // Simple user (Cassandra)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            int id = 2;

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));

            AlimentConsume resultContent;

            Func<Task> result = async () => { resultContent = await alimentConsumeService.GetOneByIdAsync(id); };

            result.Should()
                .Throw<HttpForbiddenException>()
                .WithMessage(ResourceEn.AccessDenied);

            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public void GetOneAlimentConsumeById_Bad_NotFound()
        {
            int id = 777;

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);

            AlimentConsume resultContent;

            Func<Task> result = async () => { resultContent = await _alimentConsumeService.GetOneByIdAsync(id); };

            result.Should()
                .Throw<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentConsumeNotFound);

            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void UpdateAlimentConsume_Ok()
        {
            MockUser(3); // Simple user (Benjamin)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

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

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));
            _mockAlimentConsumeRepository.Setup(m => m.UpdateAsync(updateAlimentConsume)).ReturnsAsync(afterUpdateAlimentConsume);

            var result = await alimentConsumeService.UpdateAsync(updateAlimentConsume);

            result.Should().BeEquivalentTo(afterUpdateAlimentConsume);

            _mockAlimentConsumeRepository.Verify(m => m.UpdateAsync(updateAlimentConsume), Times.Once);
            _mockAlimentConsumeRepository.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public void UpdateAlimentConsume_Bad_NotFound()
        {
            MockUser(3); // Simple user (Benjamin)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            int id = 2;
            var updateAlimentConsume = new AlimentConsume
            {
                Id = AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1).Id,
            };

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);
            _mockAlimentConsumeRepository.Setup(m => m.UpdateAsync(updateAlimentConsume)).ReturnsAsync(() => null);

            AlimentConsume resultContent;

            Func<Task> result = async () => { resultContent = await _alimentConsumeService.GetOneByIdAsync(id); };

            result.Should()
                .Throw<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentConsumeNotFound);

            _mockAlimentConsumeRepository.Verify(m => m.UpdateAsync(updateAlimentConsume), Times.Never);
            _mockAlimentConsumeRepository.Verify(m => m.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public void UpdateAlimentConsume_Bad_Forbidden()
        {
            MockUser(4); // Simple user (Cassandra)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

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

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));
            _mockAlimentConsumeRepository.Setup(m => m.UpdateAsync(updateAlimentConsume)).ReturnsAsync(afterUpdateAlimentConsume);

            AlimentConsume resultContent;

            Func<Task> result = async () => { resultContent = await alimentConsumeService.GetOneByIdAsync(id); };

            result.Should()
                .Throw<HttpForbiddenException>()
                .WithMessage(ResourceEn.AccessDenied);

            _mockAlimentConsumeRepository.Verify(m => m.UpdateAsync(updateAlimentConsume), Times.Never);
            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void DeleteAlimentConsume_Ok()
        {
            MockUser(3); // Simple user (Benjamin)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            int id = 2;

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));
            _mockAlimentConsumeRepository.Setup(m => m.DeleteAsync(id)).ReturnsAsync(true);

            var result = await alimentConsumeService.DeleteAsync(id);
            result.Should().BeTrue();

            _mockAlimentConsumeRepository.Verify(m => m.DeleteAsync(id), Times.Once);
            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public void DeleteAlimentConsume_Bad_NotFound()
        {
            MockUser(3); // Simple user (Benjamin)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            int id = 777;

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(() => null);
            _mockAlimentConsumeRepository.Setup(m => m.DeleteAsync(id)).ReturnsAsync(false);

            AlimentConsume resultContent;

            Func<Task> result = async () => { resultContent = await alimentConsumeService.GetOneByIdAsync(id); };

            result.Should()
                .Throw<HttpNotFoundException>()
                .WithMessage(ResourceEn.AlimentConsumeNotFound);

            _mockAlimentConsumeRepository.Verify(m => m.DeleteAsync(id), Times.Never);
            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public async void DeleteAlimentConsume_Ok_AdminCanPass()
        {
            MockUser(1); // Admin user (Wayne)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            int id = 2;

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));
            _mockAlimentConsumeRepository.Setup(m => m.DeleteAsync(id)).ReturnsAsync(true);

            var result = await alimentConsumeService.DeleteAsync(id);
            result.Should().BeTrue();

            _mockAlimentConsumeRepository.Verify(m => m.DeleteAsync(id), Times.Once);
            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }

        [Fact]
        public void DeleteAlimentConsume_Bad_Forbidden()
        {
            MockUser(4); // Simple user (Cassandra)
            var alimentConsumeService = new AlimentConsumeService(_mockAlimentConsumeRepository.Object, _mockHttpContextAccessor.Object);

            int id = 2;

            _mockAlimentConsumeRepository.Setup(m => m.GetOneByIdAsync(id)).ReturnsAsync(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));
            _mockAlimentConsumeRepository.Setup(m => m.DeleteAsync(id)).ReturnsAsync(true);

            bool resultContent;

            Func<Task> result = async () => { resultContent = await alimentConsumeService.DeleteAsync(id); };

            result.Should()
                .Throw<HttpForbiddenException>()
                .WithMessage(ResourceEn.AccessDenied);

            _mockAlimentConsumeRepository.Verify(m => m.DeleteAsync(id), Times.Never);
            _mockAlimentConsumeRepository.Verify(x => x.GetOneByIdAsync(id), Times.Once);
        }
    }
}
