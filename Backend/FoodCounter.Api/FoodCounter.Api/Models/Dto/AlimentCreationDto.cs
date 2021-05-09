using System.ComponentModel.DataAnnotations;

namespace FoodCounter.Api.Models.Dto
{
    /// <summary>
    /// Aliment creation model dto
    /// </summary>
    public class AlimentCreationDto
    {
        /// <summary>
        /// Name of the aliment
        /// </summary>
        /// <example>Aliment 007</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Barecode used on the package
        /// </summary>
        /// <example>1234567890123</example>
        public string Barecode { get; set; }

        /// <summary>
        /// Calories for 100g
        /// </summary>
        /// <example>456</example>
        [Required]
        public int? Calories { get; set; }
    }
}
