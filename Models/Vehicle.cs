using System.ComponentModel.DataAnnotations;

namespace ApolloEngineeringChallenge.Models
{
    public class Vehicle
    {
        [Key]
        public string VIN { get; set; }
        [Required]
        public string ManufacturerName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string HorsePower { get; set; }
        [Required]
        public string ModelName { get; set; }
        [Required]
        public int ModelYear { get; set; }
        [Required]
        public double PurchasePrice { get; set; }
        [Required]
        public string FuelType { get; set; }
        [Required]

        public string Color { get; set; }

        [Required]
        public string Category { get; set; }
        
    }
}
