using Microsoft.AspNetCore.Identity;

namespace Bogdan_Auto.Services
{
    public class RoleHelper
    {
        private RoleManager<IdentityRole> _roleManager;
        public RoleHelper(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task EnsureRoleCreated(string roleName)
        {
            if(! await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
