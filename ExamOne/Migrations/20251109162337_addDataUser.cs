using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamOne.Migrations
{
    /// <inheritdoc />
    public partial class addDataUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11651ad0-81c1-4b25-a03e-79073c0f6c88", null, "User", "USER" },
                    { "678641f1-0723-4896-94b2-be60ee829bf0", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CCCD", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProvinceCode", "SecurityStamp", "TwoFactorEnabled", "UserName", "WardCode" },
                values: new object[,]
                {
                    { "56af6f44-f85e-4b54-85b7-a4d38106dffb", 0, "123456789", "ceb63a3b-b913-4a0c-9cef-761a2f55829c", "Account", "admin@gmail.com", true, "Admin User", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEKpBXKJTMM35ihP/UvYOl5JVu6IJj2bIgygSqicVm1lwacEEb6qiyip6apNkosexGg==", null, false, "01", "21e84112-22f1-4d8e-af5a-9a46e5bd8e53", false, "admin", "001" },
                    { "701c35ef-3a49-4b22-b2fb-0c353ded7c05", 0, "987654321", "3f16d08a-5391-457c-bcc2-c827712d375e", "Account", "user1@gmail.com", true, "Normal User 1", false, null, "USER1@GMAIL.COM", "USER1", "AQAAAAIAAYagAAAAEArtSUu4lc5VA3kEbUdUJHckh3O2RgRbmTz6uPapI+4yFS9CoLE+QyyA6zlcW6xIRw==", null, false, "79", "dee0f59c-cc6d-414d-9c23-d9dad6e86098", false, "user1", "005" },
                    { "e5ba2854-c011-481d-9e8f-b20fc8e2028f", 0, "987654326", "d549c115-9f66-41d8-91e7-4f36fb6da97b", "Account", "user2@gmail.com", true, "Normal User 2", false, null, "USER2@GMAIL.COM", "USER2", "AQAAAAIAAYagAAAAEBy/pXihIM9VOpy7bR//eMrX+nbfPOIqpiHk2BWGf0vjUIcTflowqDbsVsHOmZYXuA==", null, false, "79", "2a73bea2-ad1a-4fa1-a1de-e979bd5fe231", false, "user2", "005" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "11651ad0-81c1-4b25-a03e-79073c0f6c88", "56af6f44-f85e-4b54-85b7-a4d38106dffb" },
                    { "678641f1-0723-4896-94b2-be60ee829bf0", "701c35ef-3a49-4b22-b2fb-0c353ded7c05" },
                    { "678641f1-0723-4896-94b2-be60ee829bf0", "e5ba2854-c011-481d-9e8f-b20fc8e2028f" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "11651ad0-81c1-4b25-a03e-79073c0f6c88", "56af6f44-f85e-4b54-85b7-a4d38106dffb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "678641f1-0723-4896-94b2-be60ee829bf0", "701c35ef-3a49-4b22-b2fb-0c353ded7c05" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "678641f1-0723-4896-94b2-be60ee829bf0", "e5ba2854-c011-481d-9e8f-b20fc8e2028f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11651ad0-81c1-4b25-a03e-79073c0f6c88");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "678641f1-0723-4896-94b2-be60ee829bf0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56af6f44-f85e-4b54-85b7-a4d38106dffb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "701c35ef-3a49-4b22-b2fb-0c353ded7c05");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e5ba2854-c011-481d-9e8f-b20fc8e2028f");
        }
    }
}
