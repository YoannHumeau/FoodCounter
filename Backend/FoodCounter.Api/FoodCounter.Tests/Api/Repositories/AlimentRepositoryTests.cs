using Dapper;
using Dommel;
using FluentAssertions;
using FoodCounter.Api.DataAccess.DataAccess;
using FoodCounter.Api.Models;
using FoodCounter.Api.Repositories.Implementations;
using Moq;
using Moq.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xunit;
using FoodCounter.Tests.ExampleDatas;

namespace FoodCounter.Tests.Api.Repositories
{
    public class AlimentRepositoryTests : BaseRepositoryTests
    {
        private readonly AlimentRepository _alimentRepository;

        public AlimentRepositoryTests()
        {
            _alimentRepository = new AlimentRepository(_dbAccess);
        }

        [Fact]
        public async void GetAll_Ok()
        {
            PrepareDatabase();

            var result = await _alimentRepository.GetAllAsync();

            result.Should().BeEquivalentTo(AlimentDatas.listAliments);
        }
    }
}
