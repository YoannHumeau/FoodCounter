using System.ComponentModel.DataAnnotations;

namespace FoodCounter.Api.Models.Dto
{
    /// <summary>
    /// Aliment Consume Update Model Creation Dto
    /// </summary>
    public class AlimentConsumeUpdateDto
    {
        /// <summary>
        /// Weight of the aliment consume
        /// </summary>
        [Required]
        public int? Weight { get; set; }
    }
}
