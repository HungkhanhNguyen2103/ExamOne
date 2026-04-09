using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamOne.Migrations
{
    /// <inheritdoc />
    public partial class addExamScoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "QuestionBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExamScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorrectAnswer = table.Column<int>(type: "int", nullable: false),
                    TotalAnswer = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamQuestionId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamScores", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamScores");

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

            migrationBuilder.DropColumn(
                name: "Type",
                table: "QuestionBanks");

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
    }
}
