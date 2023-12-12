using Microsoft.EntityFrameworkCore;
using VegetableShop.Data.Configurations;
using VegetableShop.Data.Entitis;

namespace VegetableShop.Data.EF
{
	public class VegetableShopDbContext: DbContext
    {
		public VegetableShopDbContext(DbContextOptions options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BillConfiguration());
			modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());
			modelBuilder.ApplyConfiguration(new CustomerConfiguration());
			modelBuilder.ApplyConfiguration(new BillDetailConfiguration());

			//Data Seeding
			//modelBuilder.Seed();
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Bill> Bills { get; set; }
		public DbSet<BillDetail> BillDetails { get; set; }
		public DbSet<Customer> Customers { get; set; }
	}
}
