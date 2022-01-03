using Microsoft.AspNetCore.Identity;

namespace Bogdan_Auto.Models
{
    public class DeliveryStaffUser:IdentityUser
    {
        public bool IsDisabled { get; set; }
    }
}
