using Microsoft.Data.Sqlite;
using System.Data;

namespace FoodCounter.Api.DataAccess.DataAccess
{
    public class DbAccess
    {
        private readonly IDbConnection _connection;

        public IDbConnection Connection { get; set; }

        public DbAccess(string connectionString = "Data Source=test.db")
        {
            var connection = new SqliteConnection(connectionString);

            connection.Open();

            Connection = connection;
        }
    }
}
