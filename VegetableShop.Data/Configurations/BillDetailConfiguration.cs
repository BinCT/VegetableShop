

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VegetableShop.Data.Entitis;

namespace VegetableShop.Data.Configurations
{
	public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetail>
	{
		public void Configure(EntityTypeBuilder<BillDetail> builder)
		{
			builder.ToTable("BillDetails");

			builder.HasKey(x => new { x.BillId, x.ProductId });

			builder.Property(x=>x.Quantity).IsRequired();

			builder.HasOne(x => x.Product).WithMany(x => x.BillDetails).HasForeignKey(x => x.ProductId);
			builder.HasOne(x => x.Bill).WithMany(x => x.BillDetails).HasForeignKey(x => x.BillId);
		}
	}
}
