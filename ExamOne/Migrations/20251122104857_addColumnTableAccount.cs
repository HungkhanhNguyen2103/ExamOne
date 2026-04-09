using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamOne.Migrations
{
    /// <inheritdoc />
    public partial class addColumnTableAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eaf9da8c-d6a1-4b94-8f68-6ab9c5ebbdc7", "1fac865d-1591-4f48-b0c7-0b2beddc14cf" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1f1726c0-4d18-4673-9e9a-e39ca6f12ad2", "6a09c63e-ba46-4f8c-911c-a0f3f14f290f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eaf9da8c-d6a1-4b94-8f68-6ab9c5ebbdc7", "c9bd6afc-7244-4a74-9cb6-2fc8aec977e1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f1726c0-4d18-4673-9e9a-e39ca6f12ad2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eaf9da8c-d6a1-4b94-8f68-6ab9c5ebbdc7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1fac865d-1591-4f48-b0c7-0b2beddc14cf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6a09c63e-ba46-4f8c-911c-a0f3f14f290f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9bd6afc-7244-4a74-9cb6-2fc8aec977e1");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f1726c0-4d18-4673-9e9a-e39ca6f12ad2", null, "Admin", "ADMIN" },
                    { "eaf9da8c-d6a1-4b94-8f68-6ab9c5ebbdc7", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BranchCode", "CCCD", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProvinceCode", "SecurityStamp", "TwoFactorEnabled", "UserName", "WardCode" },
                values: new object[,]
                {
                    { "1fac865d-1591-4f48-b0c7-0b2beddc14cf", 0, "1", "987654321", "d536de7c-35cc-4e67-9425-2b179f560012", "Account", "user1@gmail.com", true, "Normal User 1", false, null, "USER1@GMAIL.COM", "USER1", "AQAAAAIAAYagAAAAEBpkzl8jBktlTSDv8njq45Xav89wmJxZc+W6ljsWDROJnR6Z+M0uVhFbUDcIVsCfBA==", null, false, "79", "8777f6e2-4527-4b57-b571-a15dd774a017", false, "user1", "005" },
                    { "6a09c63e-ba46-4f8c-911c-a0f3f14f290f", 0, "1", "123456789", "6cf51102-4b68-4010-b5f3-9bca457b0e2c", "Account", "admin@gmail.com", true, "Admin User", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEOt/QCYusMNP+GGx8vxnIjJgbef9Cu0RNNRDUyVYnugBgVLwJ/HDwRBNBEiWftPEjw==", null, false, "01", "84ae706a-7a18-49b4-827f-d36b9ac542dc", false, "admin", "001" },
                    { "c9bd6afc-7244-4a74-9cb6-2fc8aec977e1", 0, "1", "987654326", "b4c36da4-e51a-4e6b-8a2c-c2d33fc72555", "Account", "user2@gmail.com", true, "Normal User 2", false, null, "USER2@GMAIL.COM", "USER2", "AQAAAAIAAYagAAAAEM6naKPbmRqbLX3eq7G9O8p+stuKjQFbZu6j1/L9p8rqlEthxudj7/QSiA1UnRIM6A==", null, false, "79", "c98cfc3f-d4bb-4971-ac2b-4c96f7844155", false, "user2", "005" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "eaf9da8c-d6a1-4b94-8f68-6ab9c5ebbdc7", "1fac865d-1591-4f48-b0c7-0b2beddc14cf" },
                    { "1f1726c0-4d18-4673-9e9a-e39ca6f12ad2", "6a09c63e-ba46-4f8c-911c-a0f3f14f290f" },
                    { "eaf9da8c-d6a1-4b94-8f68-6ab9c5ebbdc7", "c9bd6afc-7244-4a74-9cb6-2fc8aec977e1" }
                });
        }
    }
}
