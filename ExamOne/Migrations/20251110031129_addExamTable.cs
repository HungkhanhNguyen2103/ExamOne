using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamOne.Migrations
{
    /// <inheritdoc />
    public partial class addExamTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d0ca78f-4393-4c2c-b7e4-4a27386cdbdf", null, "User", "USER" },
                    { "78cbab47-8c13-4e4a-9379-84ad0cc54822", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CCCD", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProvinceCode", "SecurityStamp", "TwoFactorEnabled", "UserName", "WardCode" },
                values: new object[,]
                {
                    { "785ed0c7-2c2b-420b-b5ad-87185c6f84ee", 0, "123456789", "3c396861-3b6b-41fa-8227-8bea8aa18acf", "Account", "admin@gmail.com", true, "Admin User", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEPWQBu6ENKcALAaX05agQGVZ2RUJ8DSgALYpePmt8KV4YvWtIiUqN1CsklVZGX71sw==", null, false, "01", "3bef8c97-ffb9-4dcb-b57c-022861ff09ae", false, "admin", "001" },
                    { "b99d271d-f5a1-4b88-a985-798b87c24d5f", 0, "987654321", "cc36f448-3870-4755-b4b0-cfdda92e5fc3", "Account", "user1@gmail.com", true, "Normal User 1", false, null, "USER1@GMAIL.COM", "USER1", "AQAAAAIAAYagAAAAEESV3Bnygl8F68Jy0ZE3Cf/Ab+ZnGc7J0Okloboz6uzqu1CDXYyF1ubAN19h++gECA==", null, false, "79", "61da5fd0-8125-4775-bfea-c6f1dde9c926", false, "user1", "005" },
                    { "e9e5525c-ab43-4542-9bc4-72a5e019f4ba", 0, "987654326", "bc962d74-2fd4-478e-ab28-4ff37ac9d7cc", "Account", "user2@gmail.com", true, "Normal User 2", false, null, "USER2@GMAIL.COM", "USER2", "AQAAAAIAAYagAAAAEENfMWIsk0cPnl55ZJcuCaTV7sM44Ya6IcnfJYQTIfEo+9EBIItaqxGh4a6a/zteyA==", null, false, "79", "6a009d7b-746a-40ab-a9e1-4f03f130d90b", false, "user2", "005" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1d0ca78f-4393-4c2c-b7e4-4a27386cdbdf", "785ed0c7-2c2b-420b-b5ad-87185c6f84ee" },
                    { "78cbab47-8c13-4e4a-9379-84ad0cc54822", "b99d271d-f5a1-4b88-a985-798b87c24d5f" },
                    { "78cbab47-8c13-4e4a-9379-84ad0cc54822", "e9e5525c-ab43-4542-9bc4-72a5e019f4ba" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1d0ca78f-4393-4c2c-b7e4-4a27386cdbdf", "785ed0c7-2c2b-420b-b5ad-87185c6f84ee" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "78cbab47-8c13-4e4a-9379-84ad0cc54822", "b99d271d-f5a1-4b88-a985-798b87c24d5f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "78cbab47-8c13-4e4a-9379-84ad0cc54822", "e9e5525c-ab43-4542-9bc4-72a5e019f4ba" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d0ca78f-4393-4c2c-b7e4-4a27386cdbdf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78cbab47-8c13-4e4a-9379-84ad0cc54822");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "785ed0c7-2c2b-420b-b5ad-87185c6f84ee");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b99d271d-f5a1-4b88-a985-798b87c24d5f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9e5525c-ab43-4542-9bc4-72a5e019f4ba");

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
    }
}
