using System;
using System.Data;
using Dommel;
using FluentMigrator.Runner;
using FoodCounter.DbMigrations.V1;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using FoodCounter.Tests.ExampleDatas;
using FoodCounter.Api.DataAccess.DataAccess;
using Xunit;

namespace FoodCounter.Tests.Api.Repositories
{
    [Collection("Sequential")]
    public class BaseRepositoryTests
    {
        private string _connectionStringTest = "Data Source=test.db";
        public readonly DbAccess _dbAccess;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseRepositoryTests()
        {
            _dbAccess = new DbAccess(_connectionStringTest);
        }

        /// <summary>
        /// Init database
        /// </summary>
        internal void PrepareDatabase()
        {
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using var scope = serviceProvider.CreateScope();

            // Clean database before insert
            DowngradeDatabase(scope.ServiceProvider);

            // Construct database tables
            UpdateDatabase(scope.ServiceProvider);

            // Insert values in database
            using (IDbConnection connection = new SqliteConnection(_connectionStringTest))
            {
                connection.Open();

                connection.InsertAll(AlimentDatas.listAliments);
                connection.InsertAll(UserDatas.listUsers);
                connection.InsertAll(AlimentConsumeDatas.listAlimentConsumes);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSQLite()
                    // Set the connection string
                    .WithGlobalConnectionString("Data Source=test.db")
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(AddLogTable).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }

        /// <summary>
        /// Downgrade the database
        /// </summary>
        private static void DowngradeDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateDown(0);
        }
    }
}
