using doan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doan.Config
{
    public class DurationConfig : IEntityTypeConfiguration<Duration>
    {
        public void Configure(EntityTypeBuilder<Duration> builder)
        {
            builder.ToTable("Durations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.day).IsRequired(true);
            builder.Property(x => x.name).IsRequired(true).IsUnicode(true);

            builder.HasMany(e => e.productDurations)
                .WithOne(e => e.duration)
                .HasForeignKey(e => e.durationId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
