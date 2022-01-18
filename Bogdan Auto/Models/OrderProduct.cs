using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bogdan_Auto.Models


{
    public class OrderProduct
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name = "Order")]
        public int OrderId { get; set; }
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Display(Name ="Item price")]
        public double ProductIndividualPrice { get; set; }

    }
}
