

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VegetableShop.Data.Entitis;

namespace VegetableShop.Data.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Categories");

			builder.HasKey(x => x.ID);

			builder.Property(x => x.ID).UseIdentityColumn();

			builder.Property(x=>x.Name).IsRequired().HasMaxLength(100);
			builder.HasIndex(x=>x.Name).IsUnique();
		}
	}
}
