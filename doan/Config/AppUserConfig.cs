using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using doan.Entities;

namespace doan.Config
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x=>x.UserName).IsRequired(true);
            builder.HasAlternateKey(x => x.UserName);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.token).WithOne(x => x.user).HasForeignKey<UserToken>(x => x.UserId);
            builder.HasOne(x => x.login).WithOne(x => x.user).HasForeignKey<UserLogin>(x => x.UserId);
            builder.HasOne(x => x.claim).WithOne(x => x.user).HasForeignKey<UserClaim>(x => x.UserId);


        }
    }
}
