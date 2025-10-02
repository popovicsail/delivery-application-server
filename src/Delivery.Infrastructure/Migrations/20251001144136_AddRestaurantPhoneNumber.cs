using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRestaurantPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "restaurants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "dc2b5255-1d95-4e17-b254-59571a3550ae", "AQAAAAIAAYagAAAAEAw4JnwzWGUJdBZO3Ri6F/UD6AF0f2bdqurmLjryGEUtDu8o5UdPbFyWn4vfgjZ3ow==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "concurrency_stamp", "security_stamp" },
                values: new object[] { "9c5d5062-0d68-4c5b-9f71-daab6fdc6815", "1018d4a6-b76a-49c2-b072-e3d0db40077f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "97038b64-2820-483b-8d40-743f6d4364e2", "AQAAAAIAAYagAAAAENbo4rXASsirgQhbXcLBMOvTtrxu9+mBvixZHNn04yKqO6p6b9iw3sqwtC9fEVqLpw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "41551c01-3c1f-44b7-8da0-5cfb9b342cfe", "AQAAAAIAAYagAAAAENEDgPDaNJ7zUR4sZZnJS8eTdQrGCeadctrebPWi7uuJ6HvKJyFqU+o5y0aYmoUzug==" });

            migrationBuilder.UpdateData(
                table: "restaurants",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "phone_number",
                value: "065/555-333-1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "restaurants");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "d6890bb9-1bc1-471c-9bbc-7b5bf0ed70bf", "AQAAAAIAAYagAAAAEDrrwgt9L++HkACArELkjJPgasOWWkbyw1cP5CVwjDm66Drl6XL4YsWXMIQXRU3vmA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "concurrency_stamp", "security_stamp" },
                values: new object[] { "e9a7c985-d2bd-43ba-a8f4-994e56d1ce51", "4e9f22b5-121a-4490-a9d9-cd2034ab6017" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "526dce0a-b566-4d7e-8fc2-f013db9ad263", "AQAAAAIAAYagAAAAELaHwgg6I2OGkiz1hlaw1fBZE5U7N3Wmf3JBQHPvol70ybZqgV6MC7JW+H9ZmEXvqA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "8759b20d-d512-4c86-a69d-b60f25900f55", "AQAAAAIAAYagAAAAEModddbDJIBdyrGmqm3HXXTLeT0a+f88dv/DxcwdmwZNM2mr/qr8MlwEt1tXKuvXOg==" });
        }
    }
}
