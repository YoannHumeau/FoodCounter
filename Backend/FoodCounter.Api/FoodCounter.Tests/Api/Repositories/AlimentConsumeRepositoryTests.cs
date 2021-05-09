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

        public AlimentConsumeRepositoryTests()
        {
            _alimentConsumeRepository = new AlimentConsumeRepository(_dbAccess);
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
