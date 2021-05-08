namespace FoodCounter.Api.Models.Dto
{
    public class UserFullModelDto
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
    }
}
