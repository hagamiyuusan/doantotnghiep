using doan.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace doan.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product() { Id =1, Name = "API Image Captioning",Created = DateTime.Today ,}
                );
            builder.Entity<Duration>().HasData(
                new Duration() { Id = 1, name = "30 ngày", day = 30 },
                new Duration() { Id = 2, name = "90 ngày", day = 90  });
            builder.Entity<ProductDuration>().HasData(
                new ProductDuration
                {
                    Id = 1,
                    price = 3000,
                    durationId = 1,
                    productId = 1
                }, new ProductDuration
                {
                    Id = 2,
                    price = 9000,
                    durationId = 2,
                    productId = 1
                });
            var roleAdminID = new Guid("823B98EC-F77F-4CCC-A5F7-3765156B9950");
            var userID = new Guid("0790F531-8010-4BF4-8B92-0A8B7549C406");
            builder.Entity<AppRole>().HasData(
                new AppRole()
                {
                    Id = roleAdminID,
                    Name = "admin",
                    NormalizedName = "admin"
                }
                );
            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Id = userID,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "vinhhuyqna@gmail.com",
                    NormalizedEmail = "vinhhuyqna@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null,"admin123@"),
                    SecurityStamp = String.Empty

                }
                );
            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = roleAdminID,
                    UserId = userID
                }
                );
        }
    }
}
