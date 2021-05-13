using FoodCounter.Api.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCounter.Api.Models
{
    /// <summary>
    /// Weight Model
    /// </summary>
    [Table("UserWeights")]
    public class UserWeight
    {
        /// <summary>
        /// Technical id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        public long? UserId { get; set; }

        /// <summary>
        /// Date the weight was recorded
        /// </summary>
        public DateTime WeightDate { get; set; }

        /// <summary>
        /// Weight of the user
        /// </summary>
        [Required]
        public int? Weight { get; set; }
    }
}
