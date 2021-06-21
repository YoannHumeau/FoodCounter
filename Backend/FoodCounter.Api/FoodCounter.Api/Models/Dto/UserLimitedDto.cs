namespace FoodCounter.Api.Models.Dto
{
    /// <summary>
    /// User model Dto with limited public informations
    /// </summary>
    public class UserLimitedDto
    {
        /// <summary>
        /// Technical Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
    }
}
