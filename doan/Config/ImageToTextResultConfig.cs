using doan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doan.Config
{
    public class ImageToTextResultConfig : IEntityTypeConfiguration<ImageToTextResult>
    {
        public void Configure(EntityTypeBuilder<ImageToTextResult> builder)
        {
            builder.ToTable(" ImageToTextResult");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.path).IsRequired(true);

        }
    }
}
