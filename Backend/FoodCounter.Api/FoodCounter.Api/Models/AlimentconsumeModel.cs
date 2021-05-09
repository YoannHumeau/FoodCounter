using FoodCounter.Api.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodCounter.Api.Models
{
    /// <summary>
    /// Aliment consume class
    /// </summary>
    [Table("AlimentConsumes")]
    public class AlimentconsumeModel
    {
        /// <summary>
        /// Technocal Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Aliment Id
        /// </summary>
        public long AlimentId { get; set; }

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
