using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodCounter.Api.Models
{
    /// <summary>
    /// Aliment model
    /// </summary>
    [Table("aliments")]
    public class AlimentModel
    {
        /// <summary>
        /// Technical id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the aliment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Barecode used on the package
        /// </summary>
        public string Barecode { get; set; }

        /// <summary>
        /// Calories for 100g
        /// </summary>
        public int Calories { get; set; }
    }
}
