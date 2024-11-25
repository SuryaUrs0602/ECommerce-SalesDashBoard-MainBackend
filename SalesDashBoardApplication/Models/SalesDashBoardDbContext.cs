using Microsoft.EntityFrameworkCore;

namespace SalesDashBoardApplication.Models
{
    public class SalesDashBoardDbContext : DbContext
    {
        public SalesDashBoardDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserEmail)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderDate);

            modelBuilder.Entity<Revenue>()
                .HasIndex(rev => rev.Date);


            modelBuilder.Entity<SalesPerformance>()
                .HasIndex(sale => sale.Date);






            modelBuilder.Entity<User>()
                .HasMany(ord => ord.Orders)
                .WithOne(user => user.User)
                .HasForeignKey(user => user.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(ord => ord.OrderItems)
                .WithOne(ord => ord.Order)
                .HasForeignKey(ord => ord.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(ord => ord.OrderItems)
                .WithOne(pro => pro.Product)
                .HasForeignKey(pro => pro.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(inv => inv.Inventory)
                .WithOne(pro => pro.Product)
                .HasForeignKey<Inventory>(pro => pro.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<SalesPerformance> SalesPerformances { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
