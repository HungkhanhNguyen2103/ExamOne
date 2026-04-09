using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamOne.Migrations
{
    /// <inheritdoc />
    public partial class changeData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3322d4c2-8e25-469b-b3a7-22cfe784d25a", "2274d3da-da55-4360-84a0-1808ef26ce68" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ec89a145-a930-49c2-82fa-9e3b72465a1a", "8ebfe796-0e4e-43a7-9ac2-89612e20242c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3322d4c2-8e25-469b-b3a7-22cfe784d25a", "f46e9bbe-915b-40ac-8f8e-44afec0705cf" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3322d4c2-8e25-469b-b3a7-22cfe784d25a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec89a145-a930-49c2-82fa-9e3b72465a1a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2274d3da-da55-4360-84a0-1808ef26ce68");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8ebfe796-0e4e-43a7-9ac2-89612e20242c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f46e9bbe-915b-40ac-8f8e-44afec0705cf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3643d740-068c-4c58-8f78-12c0baaa3a9a", null, "User", "USER" },
                    { "49c1f1ad-fc46-4d3c-970f-d85fef3223f9", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "BranchCode", "CCCD", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProvinceCode", "SecurityStamp", "TwoFactorEnabled", "UserName", "WardCode" },
                values: new object[,]
                {
                    { "5daa27db-24d7-45c7-97c9-66bcbd07cddb", 0, "/img/user.jpg", "1", "123456789", "742da2d6-801b-45d2-805f-8e8d5d474a54", "Account", "admin@gmail.com", true, "Admin User", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAELLwtQDOndnSzmpflqhSZPGQU7kFaKsF57QxWWUYqcZi5DWGcyD5qgBj0FtBb+FJGA==", null, false, "01", "cf4d510c-79d6-46c8-a2a0-0d5e4815ecd8", false, "admin", "001" },
                    { "86e6e23d-604a-4ece-90bf-0f3909276f93", 0, "/img/user.jpg", "1", "987654326", "e9f1fd32-b755-45c0-afd6-78b5d938603b", "Account", "user2@gmail.com", true, "Normal User 2", false, null, "USER2@GMAIL.COM", "USER2", "AQAAAAIAAYagAAAAEK1LJtO5J2JDUFJqAf4iOdyRkIb52ryzVoImbsmTOUnNta3Dobtc/PxIhNN7xgL2tQ==", null, false, "79", "98924636-cb5d-4110-845b-b92d823af590", false, "user2", "005" },
                    { "a70bc3d3-1b9d-43aa-a56d-4f962fb0a61c", 0, "/img/user.jpg", "1", "987654321", "9f89b987-2286-41f2-9994-8e946c31da79", "Account", "user1@gmail.com", true, "Normal User 1", false, null, "USER1@GMAIL.COM", "USER1", "AQAAAAIAAYagAAAAEO7H/boHb9icfy3xttxI8NxidQUzgSu6dxPJarkKBwS34rpaK1fd9CNGRdFUhgJrTA==", null, false, "79", "aa7a6b3e-6033-4750-b497-1438ff973bd2", false, "user1", "005" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "49c1f1ad-fc46-4d3c-970f-d85fef3223f9", "5daa27db-24d7-45c7-97c9-66bcbd07cddb" },
                    { "3643d740-068c-4c58-8f78-12c0baaa3a9a", "86e6e23d-604a-4ece-90bf-0f3909276f93" },
                    { "3643d740-068c-4c58-8f78-12c0baaa3a9a", "a70bc3d3-1b9d-43aa-a56d-4f962fb0a61c" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "49c1f1ad-fc46-4d3c-970f-d85fef3223f9", "5daa27db-24d7-45c7-97c9-66bcbd07cddb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3643d740-068c-4c58-8f78-12c0baaa3a9a", "86e6e23d-604a-4ece-90bf-0f3909276f93" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3643d740-068c-4c58-8f78-12c0baaa3a9a", "a70bc3d3-1b9d-43aa-a56d-4f962fb0a61c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3643d740-068c-4c58-8f78-12c0baaa3a9a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49c1f1ad-fc46-4d3c-970f-d85fef3223f9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5daa27db-24d7-45c7-97c9-66bcbd07cddb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86e6e23d-604a-4ece-90bf-0f3909276f93");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a70bc3d3-1b9d-43aa-a56d-4f962fb0a61c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3322d4c2-8e25-469b-b3a7-22cfe784d25a", null, "User", "USER" },
                    { "ec89a145-a930-49c2-82fa-9e3b72465a1a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "BranchCode", "CCCD", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProvinceCode", "SecurityStamp", "TwoFactorEnabled", "UserName", "WardCode" },
                values: new object[,]
                {
                    { "2274d3da-da55-4360-84a0-1808ef26ce68", 0, "", "1", "987654321", "b27dea55-78bc-4d98-a078-1a9558e1c36e", "Account", "user1@gmail.com", true, "Normal User 1", false, null, "USER1@GMAIL.COM", "USER1", "AQAAAAIAAYagAAAAEBdd8r/Vm1GyZYhacZIAmrX6oiFGSAFpp+GI1kCpWqQMtwosleLkKwCZW7EOSahI7w==", null, false, "79", "20487b87-35b5-465d-a43b-0577dadc8e41", false, "user1", "005" },
                    { "8ebfe796-0e4e-43a7-9ac2-89612e20242c", 0, "", "1", "123456789", "6829e18a-363d-40a6-b111-1b5a70d605cb", "Account", "admin@gmail.com", true, "Admin User", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEATjBLWvtCGqtW8YTlBFF0TaaQjXQn9ucEZiD/OJdA67PbDq3igPILC95qoRO6Se7g==", null, false, "01", "b853f1dd-0d51-43c9-8464-0363edf08d46", false, "admin", "001" },
                    { "f46e9bbe-915b-40ac-8f8e-44afec0705cf", 0, "", "1", "987654326", "bbd9f834-6458-471d-b526-0612461c609a", "Account", "user2@gmail.com", true, "Normal User 2", false, null, "USER2@GMAIL.COM", "USER2", "AQAAAAIAAYagAAAAEANaMTP6NVTaoN3unhaRr6QuhzIFXcK/qgvCHUZmitrMBDYVYlafG8kXD6TQ2n/Yvg==", null, false, "79", "532f9e6b-214f-4448-b651-dad6051bacd7", false, "user2", "005" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3322d4c2-8e25-469b-b3a7-22cfe784d25a", "2274d3da-da55-4360-84a0-1808ef26ce68" },
                    { "ec89a145-a930-49c2-82fa-9e3b72465a1a", "8ebfe796-0e4e-43a7-9ac2-89612e20242c" },
                    { "3322d4c2-8e25-469b-b3a7-22cfe784d25a", "f46e9bbe-915b-40ac-8f8e-44afec0705cf" }
                });
        }
    }
}
