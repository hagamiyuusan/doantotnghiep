using doan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doan.Config
{
    public class SubscriptionConfig : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("subscriptions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.createDate).IsRequired(true);
            builder.Property(x => x.isActivate).IsRequired(true);
            builder.HasOne(x => x.productDuration).WithMany(x => x.subscriptions)
                .HasForeignKey(x => x.productDurationId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Subscriptions)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }

    }
}
