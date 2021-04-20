using Microsoft.Data.Sqlite;
using System.Data;

namespace FoodCounter.Api.DataAccess.DataAccess
{
    /// <summary>
    /// Database access class
    /// </summary>
    public class DbAccess
    {
        /// <summary>
        /// Connection to db
        /// </summary>
        public IDbConnection Connection { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="connectionString">Connectio string</param>
        public DbAccess(string connectionString)
        {
            var connection = new SqliteConnection(connectionString);

            connection.Open();

            Connection = connection;
        }
    }
}
