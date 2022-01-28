using Microsoft.AspNetCore.Identity;

namespace Bogdan_Auto.Models
{
    public class CustomerUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isAdmin { get; set; }
        public bool isDelivery { get; set; }
        public bool IsDisabled { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
