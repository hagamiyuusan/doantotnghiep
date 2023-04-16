using doan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doan.Config
{
    public class ImageForCaptioningConfig : IEntityTypeConfiguration<ImageForCaptioning>
    {
        public void Configure(EntityTypeBuilder<ImageForCaptioning> builder)
        {
            builder.ToTable("ImageForCaptioning");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.path).IsRequired(true);
            builder.HasOne(x => x.user).WithMany(x => x.imageForCaptionings).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.userId)
                .IsRequired(false);
        }
    }
}
