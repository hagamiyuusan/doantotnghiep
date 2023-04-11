using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doan.Migrations
{
    /// <inheritdoc />
    public partial class seedUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("823b98ec-f77f-4ccc-a5f7-3765156b9950"), "a6335dbe-0bff-4344-a008-ae4a13ef9c13", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("823b98ec-f77f-4ccc-a5f7-3765156b9950"), new Guid("0790f531-8010-4bf4-8b92-0a8b7549c406") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("0790f531-8010-4bf4-8b92-0a8b7549c406"), 0, "64d86425-234b-4d24-9539-a084bd44aa52", "vinhhuyqna@gmail.com", true, false, null, "vinhhuyqna@gmail.com", "admin", "AQAAAAEAACcQAAAAEISLUFWN+ivFRU8zfE3Wcgw05EDGjZrg79daRZkirZ69VvSVxk9DiRO1hP+JiEoGEg==", null, false, "", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("823b98ec-f77f-4ccc-a5f7-3765156b9950"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("823b98ec-f77f-4ccc-a5f7-3765156b9950"), new Guid("0790f531-8010-4bf4-8b92-0a8b7549c406") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("0790f531-8010-4bf4-8b92-0a8b7549c406"));
        }
    }
}
