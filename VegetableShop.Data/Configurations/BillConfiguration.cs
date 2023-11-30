using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VegetableShop.Data.Entitis;

namespace VegetableShop.Data.Configurations
{
	public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {

		public void Configure(EntityTypeBuilder<Bill> builder)
		{
			builder.ToTable("Bills");

			builder.HasKey(x => x.ID);

			builder.Property(x=>x.ID).UseIdentityColumn();
			builder.Property(x => x.CreateDay).IsRequired().HasDefaultValue(DateTime.Now);

			builder.HasOne(x => x.Customer).WithMany(x => x.Bills).HasForeignKey(x => x.CustomerId);
		}
	}
}
