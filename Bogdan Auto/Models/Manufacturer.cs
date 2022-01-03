using System.ComponentModel.DataAnnotations;

namespace Bogdan_Auto.Models
{
    public class Manufacturer
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
