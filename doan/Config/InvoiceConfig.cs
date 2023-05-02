using doan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doan.Config
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");
            builder.HasKey(x => x.Id);
            builder.HasAlternateKey(x => x.paypalId);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.isPaid).IsRequired(true);
            builder.HasOne(x=>x.appUser).WithMany(x => x.invoices)
                .HasForeignKey(x => x.userId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.productDuration).WithMany(x => x.invoices)
                .HasForeignKey(x => x.productDurationId).OnDelete(DeleteBehavior.Cascade);



        }
    }
}
