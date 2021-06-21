namespace FoodCounter.Api.Models.Dto
{
    /// <summary>
    /// User model Dto with all public informations
    /// </summary>
    public class UserFullDto
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
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public string Role { get; set; }
    }
}
