using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamOne.Migrations
{
    /// <inheritdoc />
    public partial class changeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f55ad6e6-9b20-46b2-9f81-f893d71483e6", "0081dbb0-1a0d-4d56-8d5b-ba43b46063b7" },
                    { "0ba3e0a5-9805-41ac-8167-6bf05682cc37", "ada28314-9198-4d45-b85e-a6e36c24c599" },
                    { "f55ad6e6-9b20-46b2-9f81-f893d71483e6", "b6e47df0-09eb-49f3-a96d-bcea5385b200" }
                });
        }
    }
}
