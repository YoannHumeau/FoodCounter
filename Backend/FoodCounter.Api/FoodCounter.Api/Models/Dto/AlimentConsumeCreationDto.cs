using System;
using System.ComponentModel.DataAnnotations;

namespace FoodCounter.Api.Models.Dto
{
    /// <summary>
    /// Aliment Consume Model Creation Dto
    /// </summary>
    public class AlimentConsumeCreationDto
    {
        /// <summary>
        /// Aliment Id
        /// </summary>
        [Required]
        public long AlimentId { get; set; }

        /// <summary>
        /// Date the aliment was consumed
        /// </summary>
        public DateTime? ConsumeDate { get; set; }

        /// <summary>
        /// Weight of the aliment consume
        /// </summary>
        [Required]
        public int Weight { get; set; }
    }
}
