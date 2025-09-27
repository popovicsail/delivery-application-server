using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "week_day",
                table: "work_schedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "menus",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "dishes",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "picture_url",
                table: "dishes",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "190a60d2-d461-468e-b6d9-0a47be1dee39", "AQAAAAIAAYagAAAAEO0r5DtssTgUs7PFHARgPJJ+dnECfCKp7tgv5R+fIZKQO+lbcm7f3eFQ7DTH/QY8yg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "a8a1f117-1160-4710-b063-78439e8d8c37", "AQAAAAIAAYagAAAAEAxkRQww/GUp6x+yDjmQcWTQBLCihBrAafW2W8D+sj9fRCLTd4VxLe1OjljnyqK+YA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "0d203ca2-f3d3-43a0-b13f-3897e38ab120", "AQAAAAIAAYagAAAAECqv8T8H4X04sWJsuFNxRXvGByQEqtoWFA6O0j04mag6zHS3PTpKAKAJ5ohKnyrN2w==" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "profile_picture_url", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 0, "f2f73644-d2a3-4cce-b68c-df6542bf6511", "owner1@example.com", false, "Petar", "Petrović", false, null, "OWNER1@EXAMPLE.COM", "OWNER1", "", null, false, null, "48302ba9-e90d-4dc3-adab-007314fe198d", false, "owner1" });

            migrationBuilder.InsertData(
                table: "addresses",
                columns: new[] { "id", "city", "customer_id", "postal_code", "street_and_number" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), "Beograd", null, "11000", "Knez Mihailova 12" });

            migrationBuilder.InsertData(
                table: "owners",
                columns: new[] { "id", "user_id" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.InsertData(
                table: "restaurants",
                columns: new[] { "id", "address_id", "description", "name", "owner_id" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("33333333-3333-3333-3333-333333333333"), "Autentična italijanska kuhinja sa peći na drva.", "Pizzeria Roma", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "id", "name", "restaurant_id" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), "Pizza Menu", new Guid("44444444-4444-4444-4444-444444444444") });

            migrationBuilder.InsertData(
                table: "dishes",
                columns: new[] { "id", "description", "menu_id", "name", "picture_url", "price", "type" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Pica sa paradajz sosom, sirom i bosiljkom.", new Guid("55555555-5555-5555-5555-555555555555"), "Margherita", null, 650.0, "Italian" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Pica sa šunkom, pečurkama i sirom.", new Guid("55555555-5555-5555-5555-555555555555"), "Capricciosa", null, 750.0, "Italian" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "menus",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "restaurants",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "addresses",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "owners",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DropColumn(
                name: "name",
                table: "menus");

            migrationBuilder.DropColumn(
                name: "picture_url",
                table: "dishes");

            migrationBuilder.AlterColumn<int>(
                name: "week_day",
                table: "work_schedules",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "type",
                table: "dishes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "69d9cece-11a4-4827-9ca3-2fbdae5a3634", "AQAAAAIAAYagAAAAEC2PwnairbK/z2zbraBchecUnU7kgKsk6Imi8phMg9z6fVVlAH3L8rEJ45f3PXVdTg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "d126778d-5fda-4371-8c1e-f051e911ff10", "AQAAAAIAAYagAAAAEHZBqzr2/YlMck+8Fo/lpEUEydl3vhqnVRFOi9YvR+/j78TNypWOk5WNjOM4YHDirQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "bbd40dba-eb98-4098-8bab-14be9bfae474", "AQAAAAIAAYagAAAAEFKdlE2levD0p8m6ZRYsKj3kW3S5IaEPm+lq2BAz8vM9yGOSSgmdWPYT58hITH6p/A==" });
        }
    }
}
