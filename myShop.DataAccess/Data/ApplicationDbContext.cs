using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myShop.Entities.Models;

namespace myShop.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any additional model configurations here
        }
        // Define DbSets for your entities here
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        
        
        public DbSet<OrderDetail> OrderDetails{ get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }


        public DbSet<UserVerificationCode> UserVerificationCodes { get; set; }



    }
}
