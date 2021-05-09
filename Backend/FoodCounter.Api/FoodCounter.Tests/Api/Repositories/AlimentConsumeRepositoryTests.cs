using FluentAssertions;
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
        private readonly AlimentConsumeRepository _alimentConsumeRepository;

        public AlimentConsumeRepositoryTests()
        {
            _alimentConsumeRepository = new AlimentConsumeRepository(_dbAccess);
        }

        [Fact]
        public async void GetOneAlimentById_Ok()
        {
            PrepareDatabase();

            int id = 2;

            var result = await _alimentConsumeRepository.GetOneByIdAsync(id);

            result.Should().BeEquivalentTo(AlimentConsumeDatas.listAlimentConsumes.ElementAt(id - 1));
        }

        [Fact]
        public async void GetOneAlimentById_Bad_NotFound()
        {
            PrepareDatabase();

            int id = 777;

            var result = await _alimentConsumeRepository.GetOneByIdAsync(id);

            result.Should().BeNull();
        }
    }
}
