using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Bogdan_Auto.Models
{
    public class Order
    {
        public Order()
        {
            Products = new List<OrderProduct>();
        }


        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name = "Phone Number")]

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public OrderStatus Status { get; set; }

        [Display(Name ="Total cost")]
        public double TotalCost { get; set; }

        [Display(Name = "Tracking Number")]
        public string TrackingNumber { get; set; }

        [Display(Name = "Time placed")]
        public DateTime DateTime { get; set; }
        [Display(Name = "Client Id")]
        public string ClientId { get; set; }
        public virtual List<OrderProduct> Products { get; set; }
        [ForeignKey("ClientId")]
        public virtual CustomerUser? Customer { get; set; }

    }
}
