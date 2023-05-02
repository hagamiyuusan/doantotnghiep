using doan.Config;
using doan.Entities;
using doan.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace doan.EF
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DurationConfig());
            builder.ApplyConfiguration(new ProductConfig());
            builder.ApplyConfiguration(new ProductDurationConfig());
            builder.ApplyConfiguration(new SubscriptionConfig());
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new AppRoleConfig());
            builder.ApplyConfiguration(new ImageToTextResultConfig());
            builder.ApplyConfiguration(new InvoiceConfig());
            builder.ApplyConfiguration(new TypeProductConfig());

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
                .HasKey(x => new
                {
                    x.UserId,
                    x.RoleId
                });
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins")
                .HasKey(x=>x.UserId);


            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
                .HasKey(x=>x.RoleId);
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
                .HasKey(x => x.UserId);



            builder.Seed();

        }
        public DbSet<Product> Products { set; get; }
        public DbSet<Duration> Durations { set; get; }
        public DbSet<ProductDuration> ProductDurations { set; get; }
        public DbSet<Subscription> Subscriptions { set; get; }
        public DbSet<ImageToTextResult> ImageForCaptionings { set; get; }
        public DbSet<Invoice> Invoices { set; get; }
        public DbSet<TypeProduct> typeProducts { set; get; }



    }
}
