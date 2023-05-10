using doan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doan.Config
{
    public class AppUserRoleConfig : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.ToTable("AppUserRoles");
            //builder.HasKey(x => new
            //{
            //    x.userId,
            //    x.roleId
            //});

            builder.HasOne(x=>x.user).WithMany(x=>x.roles).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x=>x.role).WithMany(x=>x.Users).HasForeignKey(x=>x.RoleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
