using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bogdan_Auto.Models
{

    public class Product
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int WarrantyPeriod { get; set; }
        public string PhotoPath { get; set; }
        public int AvailableQuantity { get; set; }
        public bool IsAvailable { get; set; }

    }
}
