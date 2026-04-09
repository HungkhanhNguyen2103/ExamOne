using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamOne.Migrations
{
    /// <inheritdoc />
    public partial class addTableBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3e9dd99c-1cc6-4433-b6b7-232b7807b473", "028e9d82-07d9-405d-8c5f-ef836090e8b7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "66a61906-30f0-4522-8f6f-674fcafd6c09", "7d0f9191-f384-4576-bdb4-cbb669d4d6ca" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3e9dd99c-1cc6-4433-b6b7-232b7807b473", "b21cb714-e591-4ba2-88bd-1cf426aade14" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e9dd99c-1cc6-4433-b6b7-232b7807b473");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66a61906-30f0-4522-8f6f-674fcafd6c09");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "028e9d82-07d9-405d-8c5f-ef836090e8b7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7d0f9191-f384-4576-bdb4-cbb669d4d6ca");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b21cb714-e591-4ba2-88bd-1cf426aade14");

            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ba3e0a5-9805-41ac-8167-6bf05682cc37", null, "Admin", "ADMIN" },
                    { "f55ad6e6-9b20-46b2-9f81-f893d71483e6", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BranchCode", "CCCD", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProvinceCode", "SecurityStamp", "TwoFactorEnabled", "UserName", "WardCode" },
                values: new object[,]
                {
                    { "0081dbb0-1a0d-4d56-8d5b-ba43b46063b7", 0, null, "987654321", "321a9745-0b70-4c12-a7ca-a5b7a045f77d", "Account", "user1@gmail.com", true, "Normal User 1", false, null, "USER1@GMAIL.COM", "USER1", "AQAAAAIAAYagAAAAECN3jNfqIcqVmnDvZxOAQtRyJegbAQCuTLHM3DQR9Lb5Hpo5BIL7CtOHVtZFta00kA==", null, false, "79", "3a62064a-1a73-46ba-8ad1-8d6c841ad794", false, "user1", "005" },
                    { "ada28314-9198-4d45-b85e-a6e36c24c599", 0, null, "123456789", "df52f37b-07d7-4076-8f6b-04195ede9d58", "Account", "admin@gmail.com", true, "Admin User", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAECWQjU+bj9DVPcDuT1+1dn6B/qGMybW2gzkblQgWLJhB2ZNIEJnNSKQFUFjt8CICWA==", null, false, "01", "f0389fed-12e1-4ba6-9143-ace1fd7243e8", false, "admin", "001" },
                    { "b6e47df0-09eb-49f3-a96d-bcea5385b200", 0, null, "987654326", "fa799473-c3bb-4627-9ff3-6cd7a66aeb11", "Account", "user2@gmail.com", true, "Normal User 2", false, null, "USER2@GMAIL.COM", "USER2", "AQAAAAIAAYagAAAAENHkuaVtfAmGFCB9XNF0PpdHIwKmxZSA2Nsgs0761pNv7PzpSZksNCPcWB4Tw2WbmA==", null, false, "79", "fc63897f-0ad7-4c03-ba56-8237478d12fb", false, "user2", "005" }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Chi đoàn A" },
                    { 2, "Chi đoàn B" },
                    { 3, "Chi đoàn C" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f55ad6e6-9b20-46b2-9f81-f893d71483e6", "0081dbb0-1a0d-4d56-8d5b-ba43b46063b7" },
                    { "0ba3e0a5-9805-41ac-8167-6bf05682cc37", "ada28314-9198-4d45-b85e-a6e36c24c599" },
                    { "f55ad6e6-9b20-46b2-9f81-f893d71483e6", "b6e47df0-09eb-49f3-a96d-bcea5385b200" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f55ad6e6-9b20-46b2-9f81-f893d71483e6", "0081dbb0-1a0d-4d56-8d5b-ba43b46063b7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0ba3e0a5-9805-41ac-8167-6bf05682cc37", "ada28314-9198-4d45-b85e-a6e36c24c599" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f55ad6e6-9b20-46b2-9f81-f893d71483e6", "b6e47df0-09eb-49f3-a96d-bcea5385b200" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ba3e0a5-9805-41ac-8167-6bf05682cc37");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f55ad6e6-9b20-46b2-9f81-f893d71483e6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0081dbb0-1a0d-4d56-8d5b-ba43b46063b7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ada28314-9198-4d45-b85e-a6e36c24c599");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b6e47df0-09eb-49f3-a96d-bcea5385b200");

            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e9dd99c-1cc6-4433-b6b7-232b7807b473", null, "Admin", "ADMIN" },
                    { "66a61906-30f0-4522-8f6f-674fcafd6c09", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CCCD", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProvinceCode", "SecurityStamp", "TwoFactorEnabled", "UserName", "WardCode" },
                values: new object[,]
                {
                    { "028e9d82-07d9-405d-8c5f-ef836090e8b7", 0, "987654326", "f9769d9b-0478-4dbc-882b-e6a822526b45", "Account", "user2@gmail.com", true, "Normal User 2", false, null, "USER2@GMAIL.COM", "USER2", "AQAAAAIAAYagAAAAEJjT+NUMh5JqEngn3rhXHPAAAzrNQ2IevGFTNqEk8cQYJGWCgOwqW/E60blx6fa1Lw==", null, false, "79", "a8a09689-c912-4218-b1af-f5825b2a18f7", false, "user2", "005" },
                    { "7d0f9191-f384-4576-bdb4-cbb669d4d6ca", 0, "123456789", "e7e640bd-be05-4984-81f3-df2b52362dc1", "Account", "admin@gmail.com", true, "Admin User", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEKt9Or1aDhLE6L//cE0IwIDe4Pa6fAgRV4pTnJZq/kgkIHYDr3O+vQTpdLcee404aQ==", null, false, "01", "cd1e48b8-bacc-43ad-9c19-15daa9d8a509", false, "admin", "001" },
                    { "b21cb714-e591-4ba2-88bd-1cf426aade14", 0, "987654321", "721bd773-d96f-4afd-81db-ac4872f4a0ac", "Account", "user1@gmail.com", true, "Normal User 1", false, null, "USER1@GMAIL.COM", "USER1", "AQAAAAIAAYagAAAAEMCqUOXFhbvhIpw9Ka3aSv3IzThyTvEieXeNmjQWBimW5tS51OFAppdauHa94JkGWQ==", null, false, "79", "384c0db0-da9e-4dce-893d-626bdfac91b0", false, "user1", "005" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3e9dd99c-1cc6-4433-b6b7-232b7807b473", "028e9d82-07d9-405d-8c5f-ef836090e8b7" },
                    { "66a61906-30f0-4522-8f6f-674fcafd6c09", "7d0f9191-f384-4576-bdb4-cbb669d4d6ca" },
                    { "3e9dd99c-1cc6-4433-b6b7-232b7807b473", "b21cb714-e591-4ba2-88bd-1cf426aade14" }
                });
        }
    }
}
