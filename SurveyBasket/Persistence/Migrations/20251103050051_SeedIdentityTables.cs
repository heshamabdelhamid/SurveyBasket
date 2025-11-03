using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurveyBasket.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "polls:read", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 2, "permissions", "polls:add", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 3, "permissions", "polls:update", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 4, "permissions", "polls:delete", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 5, "permissions", "questions:read", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 6, "permissions", "questions:add", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 7, "permissions", "questions:update", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 8, "permissions", "users:read", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 9, "permissions", "users:add", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 10, "permissions", "users:update", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 11, "permissions", "roles:read", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 12, "permissions", "roles:add", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 13, "permissions", "roles:update", "92b75286-d8f8-4061-9995-e6e23ccdee94" },
                    { 14, "permissions", "results:read", "92b75286-d8f8-4061-9995-e6e23ccdee94" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "584c0783-b3c1-4961-8db7-3365c72b3311", 0, "61b451bd-4037-4ae0-bde6-806d313f303b", "admin@survey-basket.com", true, "Survey Basket", "Admin", false, null, "ADMIN@SURVEY-BASKET.COM", "ADMIN@SURVEY-BASKET.COM", "AQAAAAIAAYagAAAAEOukAq5FFA3MDIUdRcGSV/pBYBpk8j1up/PPhGAiG0T+9B+Xh3voJdhxBv6pbU99ig==", null, false, "55BF92C9EF0249CDA210D85D1A851BC9", false, "admin@survey-basket.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "92b75286-d8f8-4061-9995-e6e23ccdee94", "584c0783-b3c1-4961-8db7-3365c72b3311" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "92b75286-d8f8-4061-9995-e6e23ccdee94", "584c0783-b3c1-4961-8db7-3365c72b3311" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "584c0783-b3c1-4961-8db7-3365c72b3311");
        }
    }
}
