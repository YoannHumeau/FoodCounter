using System;
using System.Data;
using System.Linq;

using FluentMigrator.Runner;
using FoodCounter.Api.Models;
using FoodCounter.DbMigrations.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Dommel;
using System.Diagnostics.CodeAnalysis;

namespace FoodCounter.DbMigrations
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            #region RunMigrations

            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }

            #endregion


            #region Examples

            using (IDbConnection connection = new SqliteConnection("Data Source=test.db"))
            {
                connection.Open();

                var newAliment = new Aliment
                {
                    Id = 1,
                    Name = "Aliment 01",
                    Barecode = "0123456789000000000",
                    Calories = 400
                };

                var addedAliments = connection.Insert<Aliment>(newAliment);

                var cars = connection.Select<Aliment>(p => p.Name == "Alim").SingleOrDefault();

                #endregion
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
    }
}