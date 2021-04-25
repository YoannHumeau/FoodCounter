using System.ComponentModel.DataAnnotations.Schema;

namespace FoodCounter.Api.Entities
{
    /// <summary>
    /// User class
    /// </summary>
    public class User
    {
        /// <summary>
        /// Technical Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User role (Default "User")
        /// </summary>
        public string Role { get; set; } = Entities.Role.User;

        /// <summary>
        /// User token
        /// </summary>
        [NotMapped]
        public string Token { get; set; }
    }
}
