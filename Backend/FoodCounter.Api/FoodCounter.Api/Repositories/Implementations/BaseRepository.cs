using FoodCounter.Api.DataAccess.DataAccess;
using System.Data;

namespace FoodCounter.Api.Repositories.Implementations
{
    public class BaseRepository
    {
        public readonly IDbConnection _connection;

        public BaseRepository()
        {
            var db = new DbAccess();

            _connection = db.Connection;
        }
    }
}
