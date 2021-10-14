using System.ComponentModel.DataAnnotations;

namespace FoodCounter.Api.Models.Dto
{
    public class UserUpdatePassword
    {
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
