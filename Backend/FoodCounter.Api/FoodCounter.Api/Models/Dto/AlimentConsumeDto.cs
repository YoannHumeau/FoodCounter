using System;

namespace FoodCounter.Api.Models.Dto
{
    /// <summary>
    /// Aliment Consume Model Dto class
    /// </summary>
    public class AlimentConsumeDto
    {
        /// <summary>
        /// Technocal Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Aliment linked by the AlimentId with Dommel
        /// </summary>
        public Aliment Aliment { get; set; }

        /// <summary>
        /// Date the aliment was consumed
        /// </summary>
        public DateTime ConsumeDate { get; set; }

        /// <summary>
        /// Weight of the consume
        /// </summary>
        public int Weight { get; set; }
    }
}
