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
			modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
			modelBuilder.ApplyConfiguration(new CustomerConfiguration());
			modelBuilder.ApplyConfiguration(new BillDetailConfiguration());

			//Data Seeding
			//modelBuilder.Seed();
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Bill> Bill { get; set; }
		public DbSet<BillDetail> BillDetail { get; set; }
		public DbSet<Customer> Customer { get; set; }
	}
}
