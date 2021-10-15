using System.ComponentModel.DataAnnotations;

namespace FoodCounter.Api.Models.Dto
{
    public class UserUpdatePasswordDto
    {
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
