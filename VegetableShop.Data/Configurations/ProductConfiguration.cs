using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VegetableShop.Data.Entitis;

namespace VegetableShop.Data.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>

	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id).UseIdentityColumn();
			builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
			builder.HasIndex(x => x.Name).IsUnique();
			builder.Property(x=>x.Quantity).IsRequired();
			builder.Property(x => x.Price).IsRequired();
			builder.Property(x=>x.FilePast).IsRequired();

			builder.HasOne(x=>x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId);

		}
	}
}
