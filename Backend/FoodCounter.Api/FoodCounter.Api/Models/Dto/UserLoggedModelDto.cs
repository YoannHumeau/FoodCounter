namespace FoodCounter.Api.Models.Dto
{
    /// <summary>
    /// User logged model Dto
    /// </summary>
    public class UserLoggedModelDto
    {
        /// <summary>
        /// Technical Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// User token
        /// </summary>
        public string Token { get; set; }
    }
}
