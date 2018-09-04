using Microsoft.EntityFrameworkCore;
using SpyStore.Models.Entities;

namespace SpyStore.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartRecord> ShoppingCartRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-TIATFF2\SQLEXPRESS;Database=SpyStore;Trusted_Connection=True;MultipleActiveResultSets=true;",
                    options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.EmailAddress).HasName("IX_Customers").IsUnique();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValue("getdate()");
                entity.Property(e => e.ShipDate)
                     .HasColumnType("datetime")
                     .HasDefaultValue("getdate()");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.LineItemTotal)
                    .HasColumnType("money")
                    .HasComputedColumnSql("[Quantity]*[UnitCost]");
                entity.Property(e => e.UnitCost).HasColumnType("money");
            });

        }
    }
}
