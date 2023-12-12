using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VegetableShop.Data.Entitis;

namespace VegetableShop.Data.Configurations
{
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>

	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.ToTable("Customers");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
			builder.HasIndex(x=>x.UserName).IsUnique();
			builder.Property(x=>x.UserName).IsRequired();
			builder.Property(x=>x.Password).IsRequired();
			builder.Property(x=>x.NumberPhone).IsRequired().HasMaxLength(11);
		}
	}
}
