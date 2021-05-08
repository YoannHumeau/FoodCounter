using System.ComponentModel.DataAnnotations;

namespace FoodCounter.Api.Models.Dto
{
    /// <summary>
    /// User login class Dto
    /// </summary>
    public class UserLoginModelDto
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
