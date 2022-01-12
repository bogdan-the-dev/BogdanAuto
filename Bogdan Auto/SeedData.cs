using Bogdan_Auto.Models;
using Microsoft.AspNetCore.Identity;

namespace Bogdan_Auto
{
    public static class SeedData
    {
        public static void Seed(UserManager<CustomerUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        private static void SeedUsers(UserManager<CustomerUser> userManager)
        {
            if(userManager.FindByNameAsync("admin@bogdanauto.com").Result == null)
            {
                var user = new CustomerUser
                {
                    UserName = "admin@bogdanauto.com",
                    Email = "admin@bogdanauto.com",
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(user, "defaultPassword1234!").Result;
                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator");
                }
            }
        }
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole("Administrator");
                roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync("Customer").Result)
            {
                var role = new IdentityRole("Customer");
                roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync("Delivery").Result)
            {
                var role = new IdentityRole("Delivery");
                roleManager.CreateAsync(role);
            }
        }
    }
}
