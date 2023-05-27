using doan.Config;
using doan.Entities;
using doan.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Emit;

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
            builder.ApplyConfiguration(new AppUserRoleConfig());
            builder.ApplyConfiguration(new AppUserTokenConfig());

            builder.Entity<IdentityUserRole<Guid>>()
                .ToTable("AppUserRoles")
                .HasKey(ur => new { ur.UserId, ur.RoleId });


            builder.Entity<IdentityUserClaim<Guid>>()
                
                .ToTable("AppUserClaims")
                .HasKey(x => x.Id);

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins")
                .HasKey(x=> new {x.LoginProvider,x.ProviderKey,x.UserId});
            //.HasKey(x=>x.UserId);


            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
                .HasKey(x=>x.Id);
            //.HasKey(x=>x.RoleId);
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").
                HasKey(x => new { x.LoginProvider,x.UserId,x.Name});

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
