﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using doan.EF;

#nullable disable

namespace doan.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRoleClaim<Guid>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserClaim<Guid>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginProvider", "ProviderKey", "UserId");

                    b.ToTable("AppUserLogins", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserLogin<Guid>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<Guid>");

                    b.UseTphMappingStrategy();

                    b.HasData(
                        new
                        {
                            UserId = new Guid("0790f531-8010-4bf4-8b92-0a8b7549c406"),
                            RoleId = new Guid("823b98ec-f77f-4ccc-a5f7-3765156b9950")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginProvider", "UserId", "Name");

                    b.ToTable("AppUserTokens", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserToken<Guid>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("doan.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRole", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("823b98ec-f77f-4ccc-a5f7-3765156b9950"),
                            ConcurrencyStamp = "9443c863-2644-40ab-9e18-91a6c60c45c5",
                            Name = "admin",
                            NormalizedName = "admin"
                        });
                });

            modelBuilder.Entity("doan.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("UserName");

                    b.ToTable("AppUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("0790f531-8010-4bf4-8b92-0a8b7549c406"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "97fb45b0-0012-40e8-a3f8-607429e62704",
                            Email = "vinhhuyqna@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "vinhhuyqna@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAENwsGJO7iT3f7IPCKXuCA2sF43RArZ4LhqyymcLj33DTsc4AYlMq4U7bBfNPF0RWPQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("doan.Entities.Duration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("day")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Durations", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            day = 30,
                            name = "30 ngày"
                        },
                        new
                        {
                            Id = 2,
                            day = 90,
                            name = "90 ngày"
                        },
                        new
                        {
                            Id = 3,
                            day = 120,
                            name = "90 ngày"
                        });
                });

            modelBuilder.Entity("doan.Entities.ImageToTextResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("caption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable(" ImageToTextResult", (string)null);
                });

            modelBuilder.Entity("doan.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.Property<bool>("isPaid")
                        .HasColumnType("bit");

                    b.Property<string>("paypalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("paypalIdCore")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productDurationId")
                        .HasColumnType("int");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("paypalId");

                    b.HasIndex("productDurationId");

                    b.HasIndex("userId");

                    b.ToTable("Invoices", (string)null);
                });

            modelBuilder.Entity("doan.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("API_URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("productTypeId");

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            API_URL = "",
                            Created = new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Local),
                            Name = "API Image Captioning",
                            productTypeId = 1
                        });
                });

            modelBuilder.Entity("doan.Entities.ProductDuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("durationId")
                        .HasColumnType("int");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("productId", "durationId");

                    b.HasIndex("durationId");

                    b.ToTable("ProductDurations", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            durationId = 1,
                            price = 3000,
                            productId = 1
                        },
                        new
                        {
                            Id = 2,
                            durationId = 2,
                            price = 9000,
                            productId = 1
                        });
                });

            modelBuilder.Entity("doan.Entities.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("dueDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActivate")
                        .HasColumnType("bit");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("productId");

                    b.HasIndex("userId");

                    b.ToTable("subscriptions", (string)null);
                });

            modelBuilder.Entity("doan.Entities.TypeProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            name = "Image To Text"
                        });
                });

            modelBuilder.Entity("doan.Entities.RoleClaim", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>");

                    b.HasIndex("RoleId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("RoleClaim");
                });

            modelBuilder.Entity("doan.Entities.UserClaim", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("UserClaim");
                });

            modelBuilder.Entity("doan.Entities.UserLogin", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("UserLogin");
                });

            modelBuilder.Entity("doan.Entities.AppUserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>");

                    b.HasIndex("RoleId");

                    b.ToTable("AppUserRoles", (string)null);

                    b.HasDiscriminator().HasValue("AppUserRole");
                });

            modelBuilder.Entity("doan.Entities.UserToken", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("AppUserTokens", (string)null);

                    b.HasDiscriminator().HasValue("UserToken");
                });

            modelBuilder.Entity("doan.Entities.ImageToTextResult", b =>
                {
                    b.HasOne("doan.Entities.AppUser", null)
                        .WithMany("imageToTexts")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("doan.Entities.Invoice", b =>
                {
                    b.HasOne("doan.Entities.ProductDuration", "productDuration")
                        .WithMany("invoices")
                        .HasForeignKey("productDurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("doan.Entities.AppUser", "appUser")
                        .WithMany("invoices")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appUser");

                    b.Navigation("productDuration");
                });

            modelBuilder.Entity("doan.Entities.Product", b =>
                {
                    b.HasOne("doan.Entities.TypeProduct", "typeProduct")
                        .WithMany("products")
                        .HasForeignKey("productTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("typeProduct");
                });

            modelBuilder.Entity("doan.Entities.ProductDuration", b =>
                {
                    b.HasOne("doan.Entities.Duration", "duration")
                        .WithMany("productDurations")
                        .HasForeignKey("durationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("doan.Entities.Product", "product")
                        .WithMany("productDurations")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("duration");

                    b.Navigation("product");
                });

            modelBuilder.Entity("doan.Entities.Subscription", b =>
                {
                    b.HasOne("doan.Entities.Product", "product")
                        .WithMany("subscriptions")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("doan.Entities.AppUser", "AppUser")
                        .WithMany("Subscriptions")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("product");
                });

            modelBuilder.Entity("doan.Entities.RoleClaim", b =>
                {
                    b.HasOne("doan.Entities.AppRole", "role")
                        .WithOne("claim")
                        .HasForeignKey("doan.Entities.RoleClaim", "RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("role");
                });

            modelBuilder.Entity("doan.Entities.UserClaim", b =>
                {
                    b.HasOne("doan.Entities.AppUser", "user")
                        .WithOne("claim")
                        .HasForeignKey("doan.Entities.UserClaim", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("doan.Entities.UserLogin", b =>
                {
                    b.HasOne("doan.Entities.AppUser", "user")
                        .WithOne("login")
                        .HasForeignKey("doan.Entities.UserLogin", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("doan.Entities.AppUserRole", b =>
                {
                    b.HasOne("doan.Entities.AppRole", "role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("doan.Entities.AppUser", "user")
                        .WithMany("roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("role");

                    b.Navigation("user");
                });

            modelBuilder.Entity("doan.Entities.UserToken", b =>
                {
                    b.HasOne("doan.Entities.AppUser", "user")
                        .WithOne("token")
                        .HasForeignKey("doan.Entities.UserToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("doan.Entities.AppRole", b =>
                {
                    b.Navigation("Users");

                    b.Navigation("claim")
                        .IsRequired();
                });

            modelBuilder.Entity("doan.Entities.AppUser", b =>
                {
                    b.Navigation("Subscriptions");

                    b.Navigation("claim")
                        .IsRequired();

                    b.Navigation("imageToTexts");

                    b.Navigation("invoices");

                    b.Navigation("login")
                        .IsRequired();

                    b.Navigation("roles");

                    b.Navigation("token")
                        .IsRequired();
                });

            modelBuilder.Entity("doan.Entities.Duration", b =>
                {
                    b.Navigation("productDurations");
                });

            modelBuilder.Entity("doan.Entities.Product", b =>
                {
                    b.Navigation("productDurations");

                    b.Navigation("subscriptions");
                });

            modelBuilder.Entity("doan.Entities.ProductDuration", b =>
                {
                    b.Navigation("invoices");
                });

            modelBuilder.Entity("doan.Entities.TypeProduct", b =>
                {
                    b.Navigation("products");
                });
#pragma warning restore 612, 618
        }
    }
}
