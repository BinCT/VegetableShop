using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace VegetableShop.Data.EF
{
	public class VegetableShopDbContextFactory : IDesignTimeDbContextFactory<VegetableShopDbContext>
	{
		public VegetableShopDbContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
			   .SetBasePath(Directory.GetCurrentDirectory())
			   .AddJsonFile("appsettings.json")
			   .Build();
			var connetString = configuration.GetConnectionString("VegetableShopDb");
			var optionsBuilder = new DbContextOptionsBuilder<VegetableShopDbContext>();
			optionsBuilder.UseSqlServer(connetString);
			return new VegetableShopDbContext(optionsBuilder.Options);
		}
	}
}
