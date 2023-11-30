

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VegetableShop.Data.Entitis;

namespace VegetableShop.Data.Configurations
{
	public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>

	{
		public void Configure(EntityTypeBuilder<ProductImage> builder)
		{
			builder.ToTable("ProductImages");

			builder.HasKey(x => x.Id);

			builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);

			builder.HasOne(x => x.Product).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductId);
		}
	}
}
