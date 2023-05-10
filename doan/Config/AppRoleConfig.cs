using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using doan.Entities;

namespace doan.Config
{
    public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRoles");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.claim).WithOne(x => x.role).HasForeignKey<RoleClaim>(x => x.RoleId);



        }
    }
}
