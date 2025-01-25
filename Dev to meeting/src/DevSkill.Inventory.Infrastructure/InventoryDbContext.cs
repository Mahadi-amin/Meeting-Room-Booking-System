using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure
{
    public class InventoryDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public InventoryDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    x => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands{ get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<BusinessLocation> BusinessLocations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductLocation> ProductLocations { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<StockTransfer> StockTransfers { get; set; }
    }
}
