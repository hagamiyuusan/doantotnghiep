using doan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doan.Config
{
    public class TypeProductConfig : IEntityTypeConfiguration<TypeProduct>
    {
        public void Configure(EntityTypeBuilder<TypeProduct> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            //builder.HasMany(x => x.products).WithOne(x => x.typeProduct)
            //    .HasForeignKey(x => x.productTypeId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
