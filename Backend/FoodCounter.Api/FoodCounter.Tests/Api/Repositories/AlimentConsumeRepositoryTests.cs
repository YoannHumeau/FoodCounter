using FluentAssertions;
using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Repositories.Implementations;
using FoodCounter.Tests.ExampleDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FoodCounter.Tests.Api.Repositories
{
    public class AlimentConsumeRepositoryTests : BaseRepositoryTests
    {
        private readonly IAlimentConsumeRepository _alimentConsumeRepository;
        private readonly IAlimentRepository _alimentRepository;

        public AlimentConsumeRepositoryTests()
        {
            _alimentRepository = new AlimentRepository(_dbAccess);
            _alimentConsumeRepository = new AlimentConsumeRepository(_dbAccess, _alimentRepository);
        }

        [Fact]
        public async void CreateAliment_Ok()
        {
            PrepareDatabase();

            var result = await _alimentConsumeRepository.CreateAsync(AlimentConsumeDatas.newAlimentConsume);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.newAlimentConsumeCreated);

            var resultCheck = await _alimentConsumeRepository.GetOneByIdAsync(AlimentConsumeDatas.newAlimentConsumeCreated.Id);
            resultCheck.Should().BeEquivalentTo(AlimentConsumeDatas.newAlimentConsumeCreated);
        }

        [Fact]
        public async void GetAllAlimentsConsume_Ok()
        {
            PrepareDatabase();

            long userId = 3;

            var result = await _alimentConsumeRepository.GetAllByUserIdAsync(userId);

            var resultExpected = AlimentConsumeDatas.listAlimentConsumes.Where(ac => ac.UserId == userId);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.listAlimentConsumes.Where(ac => ac.UserId == userId));
        }

        [Fact]
        public async void GetAllAlimentsConsume_Bad_ResultNull()
        {
            PrepareDatabase();

            long userId = 2;

            var result = await _alimentConsumeRepository.GetAllByUserIdAsync(userId);

            result.Should().BeEquivalentTo(new List<AlimentConsume>());
        }

        [Fact]
        public async void GetOneAlimentConsumeById_Ok()
        {
            PrepareDatabase();

            int id = 2;

            var result = await _alimentConsumeRepository.GetOneByIdAsync(id);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));
        }

        [Fact]
        public async void GetOneAlimentConsumeById_Bad_NotFound()
        {
            PrepareDatabase();

            int id = 777;

            var result = await _alimentConsumeRepository.GetOneByIdAsync(id);

            result.Should().BeNull();
        }
    }
}
