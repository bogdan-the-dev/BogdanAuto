using Bogdan_Auto.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bogdan_Auto.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomerUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerUser> CustomerUsers { get; set;}
        public DbSet<Product> Products { get; set;}
        public DbSet<Manufacturer> Manufacturers { get; set;}
        public DbSet<Category> Categories { get; set;}
        public DbSet<AdminUser> AdminUsers { get; set;}
        public DbSet<DeliveryStaffUser> DeliveryStaffUsers { get;set;}
    }
}