﻿using doan.Entities;
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
            builder.Property(x => x.dueDate).IsRequired(true);
            builder.Property(x => x.isActivate).IsRequired(true);
            builder.HasOne(x => x.product).WithMany(x => x.subscriptions)
                .HasForeignKey(x => x.productId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Subscriptions)
                .HasForeignKey(x => x.userId).OnDelete(DeleteBehavior.Cascade);
        }

    }
}
