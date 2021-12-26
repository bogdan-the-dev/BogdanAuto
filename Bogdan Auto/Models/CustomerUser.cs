using Microsoft.AspNetCore.Identity;

namespace Bogdan_Auto.Models
{
    public class CustomerUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
