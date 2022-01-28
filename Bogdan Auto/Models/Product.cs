using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Bogdan_Auto.Models
{

    public class Product
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Category? Category { get; set; }
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Manufacturer? Manufacturer { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Range(0, int.MaxValue)]
        public int WarrantyPeriod { get; set; }
        public string? PhotoPath { get; set; }
        [Range(0, int.MaxValue)]
        public int AvailableQuantity { get; set; }
        public bool IsAvailable { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public IEnumerable<OrderProduct>? orderProducts { get; set; }
    }
}
