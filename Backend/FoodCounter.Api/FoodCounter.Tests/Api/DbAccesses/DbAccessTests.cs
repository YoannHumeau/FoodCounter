using FluentAssertions;
using FoodCounter.Api.DataAccess.DataAccess;
using System;
using Xunit;
using FoodCounter.Api.Resources;

namespace FoodCounter.Tests.Api.DbAccesses
{
    [Collection("Sequential")]
    public class DbAccessTest
    {
        [Fact]
        public void DbAccess_Bad_BadDatabaseType()
        {
            string connetionDatabaseType = "troll-type";
            string connectionStringTest = "Data Source=test.db";

            Assert.Throws<Exception>(() => new DbAccess(connetionDatabaseType, connectionStringTest))
                .Message.Should().BeEquivalentTo(ResourceEn.DbError);
        }
    }
}
