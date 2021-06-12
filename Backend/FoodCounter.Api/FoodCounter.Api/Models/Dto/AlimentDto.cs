namespace FoodCounter.Api.Models.Dto
{
    public class AlimentDto
    {
        /// <summary>
        /// Technical id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the aliment
        /// </summary>
        /// <example>Aliment 1</example>
        public string Name { get; set; }

        /// <summary>
        /// Barecode used on the package
        /// </summary>
        /// <example>1234567890123</example>
        public string Barecode { get; set; }

        /// <summary>
        /// Calories for 100g
        /// </summary>
        /// <example>777</example>
        public int Calories { get; set; }
    }
}
