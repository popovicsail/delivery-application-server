using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllergenSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "allergens",
                columns: new[] { "id", "name", "type" },
                values: new object[,]
                {
            { new Guid("11111111-1111-1111-1111-111111111111"), "Kikiriki", "Orašasti plodovi" },
            { new Guid("12121212-1212-1212-1212-121212121212"), "Jagode", "Voće" },
            { new Guid("13131313-1313-1313-1313-131313131313"), "Banane", "Voće" },
            { new Guid("14141414-1414-1414-1414-141414141414"), "Kivi", "Voće" },
            { new Guid("15151515-1515-1515-1515-151515151515"), "Breskve", "Voće" },
            { new Guid("16161616-1616-1616-1616-161616161616"), "Paradajz", "Povrće" },
            { new Guid("22222222-2222-2222-2222-222222222222"), "Orašasti plodovi", "Orašasti plodovi" },
            { new Guid("33333333-3333-3333-3333-333333333333"), "Mleko", "Mlečni proizvodi" },
            { new Guid("44444444-4444-4444-4444-444444444444"), "Jaja", "Životinjski proizvodi" },
            { new Guid("55555555-5555-5555-5555-555555555555"), "Riba", "Morski plodovi" },
            { new Guid("66666666-6666-6666-6666-666666666666"), "Školjke", "Morski plodovi" },
            { new Guid("77777777-7777-7777-7777-777777777777"), "Rakovi", "Morski plodovi" },
            { new Guid("88888888-8888-8888-8888-888888888888"), "Soja", "Mahunarke" },
            { new Guid("99999999-9999-9999-9999-999999999999"), "Pšenica", "Žitarice" },
            { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Gluten", "Žitarice" },
            { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Sezam", "Seme" },
            { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Sulfiti", "Dodaci" },
            { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Celer", "Povrće" },
            { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "Senf", "Začini" },
            { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "Lupina (lupinovo brašno)", "Mahunarke" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("12121212-1212-1212-1212-121212121212"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("13131313-1313-1313-1313-131313131313"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("14141414-1414-1414-1414-141414141414"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("15151515-1515-1515-1515-151515151515"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("16161616-1616-1616-1616-161616161616"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));
        }

    }
}
