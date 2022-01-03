using System.ComponentModel.DataAnnotations;

namespace Bogdan_Auto.Models
{
    public class Category
    {
       [ScaffoldColumn(false)]
       public int Id { get; set; }
        public string Name { get; set; }
    }
}
