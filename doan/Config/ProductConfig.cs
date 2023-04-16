using doan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doan.Config
{
    public class ProductConfig:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired(true).IsUnicode(true);
            builder.Property(x => x.Created).IsRequired(true);
            builder.Property(x => x.API_URL).IsRequired(true);
            builder.HasMany(e=>e.productDurations)
                .WithOne(e=>e.product)
                .HasForeignKey(e=>e.productId)
                .OnDelete(DeleteBehavior.Cascade);

            

        }
    }
}
