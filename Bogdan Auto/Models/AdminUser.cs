using Microsoft.AspNetCore.Identity;

namespace Bogdan_Auto.Models
{
    public class AdminUser:IdentityUser
    {
        public bool IsDisabled { get; set; }
    }
}
