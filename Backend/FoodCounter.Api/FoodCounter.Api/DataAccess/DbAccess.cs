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
        /// <param name="connectionType">Type of the database</param>
        /// <param name="connectionString">Connection string</param>
        public DbAccess(string connectionType, string connectionString)
        {
            IDbConnection connection;

            switch (connectionType)
            {
                case "sqlite":
                    connection = new SqliteConnection(connectionString);
                    connection.Open();
                    break;
                default:
                    // TODO : Put some error logs here
                    throw new System.Exception(Resources.ResourceEn.DbError);
            }

            Connection = connection;
        }
    }
}
