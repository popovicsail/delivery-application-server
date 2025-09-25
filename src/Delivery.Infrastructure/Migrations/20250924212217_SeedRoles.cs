using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { new Guid("190d206e-0b99-4d0f-b3fa-da6ceea6d8cb"), null, "Courier", "COURIER" },
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("5b00155d-77a2-438c-b18f-dc1cc8af5a43"), null, "Customer", "CUSTOMER" },
                    { new Guid("f09ece5a-1c11-4792-815b-4ef1bc6c6c20"), null, "Worker", "WORKER" },
                    { new Guid("fc7e84f2-e37e-46e2-a222-a839d3e1a3bb"), null, "Owner", "OWNER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "profile_picture_url", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[,]
                {
                    { new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"), 0, "69d9cece-11a4-4827-9ca3-2fbdae5a3634", "admin@example3.com", true, "Glavni", "Admin", false, null, "ADMIN@EXAMPLE3.COM", "ADMIN3", "AQAAAAIAAYagAAAAEC2PwnairbK/z2zbraBchecUnU7kgKsk6Imi8phMg9z6fVVlAH3L8rEJ45f3PXVdTg==", null, true, null, null, false, "admin3" },
                    { new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"), 0, "d126778d-5fda-4371-8c1e-f051e911ff10", "admin@example1.com", true, "Glavni", "Admin", false, null, "ADMIN@EXAMPLE1.COM", "ADMIN1", "AQAAAAIAAYagAAAAEHZBqzr2/YlMck+8Fo/lpEUEydl3vhqnVRFOi9YvR+/j78TNypWOk5WNjOM4YHDirQ==", null, true, null, null, false, "admin1" },
                    { new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"), 0, "bbd40dba-eb98-4098-8bab-14be9bfae474", "admin@example2.com", true, "Glavni", "Admin", false, null, "ADMIN@EXAMPLE2.COM", "ADMIN2", "AQAAAAIAAYagAAAAEFKdlE2levD0p8m6ZRYsKj3kW3S5IaEPm+lq2BAz8vM9yGOSSgmdWPYT58hITH6p/A==", null, true, null, null, false, "admin2" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334") },
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7") },
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: new Guid("190d206e-0b99-4d0f-b3fa-da6ceea6d8cb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: new Guid("5b00155d-77a2-438c-b18f-dc1cc8af5a43"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: new Guid("f09ece5a-1c11-4792-815b-4ef1bc6c6c20"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: new Guid("fc7e84f2-e37e-46e2-a222-a839d3e1a3bb"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"));
        }
    }
}
