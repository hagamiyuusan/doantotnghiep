using doan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doan.Config
{
    public class ProductDurationConfig: IEntityTypeConfiguration<ProductDuration>
    {
        public void Configure(EntityTypeBuilder<ProductDuration> builder)
        {
            builder.ToTable("ProductDurations")
                .HasKey(x => x.Id);
            builder.HasAlternateKey(x => new { x.productId, x.durationId });

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.product).WithMany(x => x.productDurations)
                .HasForeignKey(x => x.productId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x=>x.duration).WithMany(x=>x.productDurations)
                .HasForeignKey(x=>x.durationId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.price).IsRequired(true);
           
        }

    }
}
