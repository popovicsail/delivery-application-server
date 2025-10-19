using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("1ac9bd22-e931-4b4e-9ad4-40c09a6d4e93"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("200f901d-0a1a-4975-8acf-2fda06938db7"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("8c2abe08-31ba-4e96-a1e6-bb5de1747318"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("8f08c908-4081-404d-adbf-73bae6a62f09"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("8f43d4ee-52d6-4fdc-936c-d870278403ec"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("a367885b-e302-418a-8adc-564fd051bcb3"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("c2a5d8bd-07fb-43dd-bad4-e3658499d1c2"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("c58e8b18-0c84-4fb6-9a35-77678cfe1419"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("f53ec0b4-73eb-493c-8bc2-2b44c409173a"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("fef00575-178e-4f7a-b959-cef33e76d529"));

            migrationBuilder.DeleteData(
                table: "base_work_scheds",
                keyColumn: "id",
                keyValue: new Guid("8c193535-8ac4-460b-8e77-3a3bd451e6d4"));

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: new Guid("56669f95-63d9-40e7-b548-d58337db2054"));

            migrationBuilder.DeleteData(
                table: "vouchers",
                keyColumn: "id",
                keyValue: new Guid("f44e0adf-9c57-4fb4-bed9-83ab4e87f7c9"));

            migrationBuilder.DeleteData(
                table: "menus",
                keyColumn: "id",
                keyValue: new Guid("b7575dd8-93d5-44c8-a7b4-656a3c8f2f62"));

            migrationBuilder.DeleteData(
                table: "restaurants",
                keyColumn: "id",
                keyValue: new Guid("bce87730-d28c-4576-9c90-19d424f5ec5c"));

            migrationBuilder.DeleteData(
                table: "addresses",
                keyColumn: "id",
                keyValue: new Guid("5a59266e-d819-4f57-97d1-f32ff012a16f"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "68d8fa3a-4357-41ae-ae0f-1c16f5f54766", "AQAAAAIAAYagAAAAEI049uAJM05wWzL3kYNRBsm1sM4krMwybZu1ei6c18N1uLCL1kVIgjZw7JUGOFm1ew==", "68e6b4c7-f855-4107-afaf-2e3cce8a42de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "8384108e-c0e8-4524-8576-f5fc07f36b32", "AQAAAAIAAYagAAAAEB03BfyTzTdxDxVhlX0UwTYpn38+kwvriebsA0pAmopQFx9Eg16Ewl9mIOrVvq96yA==", "ba164b76-7927-40bf-9f98-ccaecea67393" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "concurrency_stamp", "date_of_birth", "password_hash", "security_stamp" },
                values: new object[] { "36a113a9-cdbb-4500-ac99-b3c6f0ce4952", new DateTime(2025, 10, 19, 16, 52, 5, 617, DateTimeKind.Utc).AddTicks(468), "AQAAAAIAAYagAAAAEP5BQQBCkf6kfC5PL5LzLQEcRKguciN1dOFmZyoubeYXH/LWXOpMSyOvj0SRr46VQA==", "2d6fdaa6-142b-4331-ab94-cd60d4f9f4d3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "def7644a-0d7d-4c13-97c9-ff68135ee32f", "AQAAAAIAAYagAAAAENaaX4XSDgmCzr/1FCvRhKwK8yKzMeXM1fA2GKpqXEhXZ/zOKkuKmYQq/4zF3kVBFg==", "02cb8be2-c06f-4c83-b5e3-6c97e22c1048" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "b2c40af0-2248-4906-8487-1e055003ac56", "AQAAAAIAAYagAAAAEBsBd2dDKhMmO3xK5QwlmX/FyBTVRT0RhbcVXCyiN7gRw6GBCmBfPlBa9be8Y13BWg==", "d6ab3b46-60a7-44bb-928e-69d0149fa60a" });

            migrationBuilder.InsertData(
                table: "addresses",
                columns: new[] { "id", "city", "customer_id", "postal_code", "street_and_number" },
                values: new object[] { new Guid("096f786f-2fd7-4da2-ba51-93e216f3bca4"), "Beograd", null, "11000", "Knez Mihailova 12" });

            migrationBuilder.InsertData(
                table: "allergens",
                columns: new[] { "id", "name", "type" },
                values: new object[,]
                {
                    { new Guid("10637139-edb1-43ed-82a1-2bd063c2f90b"), "Breskve", "Voće" },
                    { new Guid("16c3b6e7-7ef7-45ab-b1c6-5ac95d56fba0"), "Celer", "Povrće" },
                    { new Guid("36d47a95-8491-4b7a-8c29-1e8cf1ee1fc9"), "Jagode", "Voće" },
                    { new Guid("4c0f7ecb-61ff-44ae-a7cd-ec62c25b1434"), "Gluten", "Žitarice" },
                    { new Guid("5ccfb2ed-8ea7-4ada-8002-1d6eb67808d1"), "Pšenica", "Žitarice" },
                    { new Guid("79a01309-02be-495f-bfa5-d6c79e764037"), "Banane", "Voće" },
                    { new Guid("a7a182f5-3220-4588-9bbe-e66cdef82ad1"), "Kikiriki", "Orašasti plodovi" },
                    { new Guid("c537ee9e-2083-442f-adb5-2bf82f0d26ee"), "Paradajz", "Povrće" },
                    { new Guid("e3e1261a-f8fa-47fe-aa0f-5e428977f2f8"), "Školjke", "Morski plodovi" },
                    { new Guid("e53b5c60-5aad-4ae9-96db-e7e9b0b6772d"), "Kivi", "Voće" }
                });

            migrationBuilder.InsertData(
                table: "vouchers",
                columns: new[] { "id", "active", "code", "customer_id", "date_issued", "discount_amount", "expiration_date", "name" },
                values: new object[] { new Guid("ea825fce-ec4c-43be-899c-9cdf52cf9e91"), true, "NX6PD", new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 10, 19, 16, 52, 5, 651, DateTimeKind.Utc).AddTicks(6842), 1200.0, new DateTime(2025, 10, 20, 16, 52, 5, 651, DateTimeKind.Utc).AddTicks(6835), "TestVaučer" });

            migrationBuilder.InsertData(
                table: "restaurants",
                columns: new[] { "id", "address_id", "base_work_sched_id", "description", "image", "name", "owner_id", "phone_number" },
                values: new object[] { new Guid("fa00f1dd-1b10-4733-9101-8b56da6292db"), new Guid("096f786f-2fd7-4da2-ba51-93e216f3bca4"), new Guid("19c13060-7b5e-43c2-afe9-de03ec3f29ea"), "Autentična italijanska kuhinja.", "", "Pizzeria Roma", new Guid("33333333-3333-3333-3333-333333333333"), "222" });

            migrationBuilder.InsertData(
                table: "base_work_scheds",
                columns: new[] { "id", "restaurant_id", "saturday", "sunday", "weekend_end", "weekend_start", "work_day_end", "work_day_start" },
                values: new object[] { new Guid("19c13060-7b5e-43c2-afe9-de03ec3f29ea"), new Guid("fa00f1dd-1b10-4733-9101-8b56da6292db"), true, true, new TimeSpan(0, 21, 30, 0, 0), new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "id", "name", "restaurant_id" },
                values: new object[] { new Guid("fbb11f94-a83c-4a1d-aeab-4f7bba79a720"), "Pizza Menu", new Guid("fa00f1dd-1b10-4733-9101-8b56da6292db") });

            migrationBuilder.InsertData(
                table: "dishes",
                columns: new[] { "id", "description", "menu_id", "name", "picture_url", "price", "type" },
                values: new object[] { new Guid("12cf9a99-4fee-4da5-b4bc-0cb3edfccd37"), "Pica sa šunkom i sirom.", new Guid("fbb11f94-a83c-4a1d-aeab-4f7bba79a720"), "Capricciosa", null, 750.0, "Pizza" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("10637139-edb1-43ed-82a1-2bd063c2f90b"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("16c3b6e7-7ef7-45ab-b1c6-5ac95d56fba0"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("36d47a95-8491-4b7a-8c29-1e8cf1ee1fc9"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("4c0f7ecb-61ff-44ae-a7cd-ec62c25b1434"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("5ccfb2ed-8ea7-4ada-8002-1d6eb67808d1"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("79a01309-02be-495f-bfa5-d6c79e764037"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("a7a182f5-3220-4588-9bbe-e66cdef82ad1"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("c537ee9e-2083-442f-adb5-2bf82f0d26ee"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("e3e1261a-f8fa-47fe-aa0f-5e428977f2f8"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("e53b5c60-5aad-4ae9-96db-e7e9b0b6772d"));

            migrationBuilder.DeleteData(
                table: "base_work_scheds",
                keyColumn: "id",
                keyValue: new Guid("19c13060-7b5e-43c2-afe9-de03ec3f29ea"));

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: new Guid("12cf9a99-4fee-4da5-b4bc-0cb3edfccd37"));

            migrationBuilder.DeleteData(
                table: "vouchers",
                keyColumn: "id",
                keyValue: new Guid("ea825fce-ec4c-43be-899c-9cdf52cf9e91"));

            migrationBuilder.DeleteData(
                table: "menus",
                keyColumn: "id",
                keyValue: new Guid("fbb11f94-a83c-4a1d-aeab-4f7bba79a720"));

            migrationBuilder.DeleteData(
                table: "restaurants",
                keyColumn: "id",
                keyValue: new Guid("fa00f1dd-1b10-4733-9101-8b56da6292db"));

            migrationBuilder.DeleteData(
                table: "addresses",
                keyColumn: "id",
                keyValue: new Guid("096f786f-2fd7-4da2-ba51-93e216f3bca4"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "fad911f2-7d57-4d38-8e3d-a0be4a40af8b", "AQAAAAIAAYagAAAAEJ9Ta3edzFpm5O5XMyCtkGMzWmw1j76/i4FAEsqRk0UlpexaLkfGnmxK1Nu2eTwtEA==", "6f4fbab6-f171-44c2-86bd-f674ada78807" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "bb2929a9-4a39-45d9-82ca-72b636ae02ba", "AQAAAAIAAYagAAAAEEHLlyvHthFf1LuwJTQF0Mdv15ts4RHtFz29OQX8eGM4NSzS0OFdUNMTiJTXgk0nBA==", "f4d2d9f9-f8c8-406c-809a-d9fff565b5f3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "concurrency_stamp", "date_of_birth", "password_hash", "security_stamp" },
                values: new object[] { "0117b0bb-18ae-4304-a281-a57d36139b00", null, "AQAAAAIAAYagAAAAEBJ0AgE7pY2qVlfTAvxfgaPhG2Ws11p6pNKGQRH9oioWxDbguwFd3FGq3tu7PcvDGw==", "b9313374-6f8c-4318-9341-b7cc39108a4b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "5a310510-ea2a-4aba-bf4a-f3d9de35d502", "AQAAAAIAAYagAAAAEKvNDCinEv5G0ZdtZJGpKJLyQpwnjtPLsnOW2tKABalpWyjctBSqv9pmhe0Hv6aXVQ==", "72f4e3b7-0ea9-477f-8e54-6f131a792f9d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "62495f44-4798-47e0-baf9-8445c5bc7a4b", "AQAAAAIAAYagAAAAEC2ZZvNwV5NqBZ06OCn0Vf++9GckDDdWQ/S7zzqfWXvAnFA8HIIWgrfoVaQV1ALuew==", "aed5c7b0-ab8a-4bd1-9a6d-f16a8cc4c877" });

            migrationBuilder.InsertData(
                table: "addresses",
                columns: new[] { "id", "city", "customer_id", "postal_code", "street_and_number" },
                values: new object[] { new Guid("5a59266e-d819-4f57-97d1-f32ff012a16f"), "Beograd", null, "11000", "Knez Mihailova 12" });

            migrationBuilder.InsertData(
                table: "allergens",
                columns: new[] { "id", "name", "type" },
                values: new object[,]
                {
                    { new Guid("1ac9bd22-e931-4b4e-9ad4-40c09a6d4e93"), "Banane", "Voće" },
                    { new Guid("200f901d-0a1a-4975-8acf-2fda06938db7"), "Kikiriki", "Orašasti plodovi" },
                    { new Guid("8c2abe08-31ba-4e96-a1e6-bb5de1747318"), "Celer", "Povrće" },
                    { new Guid("8f08c908-4081-404d-adbf-73bae6a62f09"), "Pšenica", "Žitarice" },
                    { new Guid("8f43d4ee-52d6-4fdc-936c-d870278403ec"), "Paradajz", "Povrće" },
                    { new Guid("a367885b-e302-418a-8adc-564fd051bcb3"), "Školjke", "Morski plodovi" },
                    { new Guid("c2a5d8bd-07fb-43dd-bad4-e3658499d1c2"), "Breskve", "Voće" },
                    { new Guid("c58e8b18-0c84-4fb6-9a35-77678cfe1419"), "Jagode", "Voće" },
                    { new Guid("f53ec0b4-73eb-493c-8bc2-2b44c409173a"), "Gluten", "Žitarice" },
                    { new Guid("fef00575-178e-4f7a-b959-cef33e76d529"), "Kivi", "Voće" }
                });

            migrationBuilder.InsertData(
                table: "vouchers",
                columns: new[] { "id", "active", "code", "customer_id", "date_issued", "discount_amount", "expiration_date", "name" },
                values: new object[] { new Guid("f44e0adf-9c57-4fb4-bed9-83ab4e87f7c9"), true, "ZJVLJ", new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 10, 19, 16, 44, 55, 156, DateTimeKind.Utc).AddTicks(5583), 1200.0, new DateTime(2025, 10, 20, 16, 44, 55, 156, DateTimeKind.Utc).AddTicks(5571), "TestVaučer" });

            migrationBuilder.InsertData(
                table: "restaurants",
                columns: new[] { "id", "address_id", "base_work_sched_id", "description", "image", "name", "owner_id", "phone_number" },
                values: new object[] { new Guid("bce87730-d28c-4576-9c90-19d424f5ec5c"), new Guid("5a59266e-d819-4f57-97d1-f32ff012a16f"), new Guid("8c193535-8ac4-460b-8e77-3a3bd451e6d4"), "Autentična italijanska kuhinja.", "", "Pizzeria Roma", new Guid("33333333-3333-3333-3333-333333333333"), "222" });

            migrationBuilder.InsertData(
                table: "base_work_scheds",
                columns: new[] { "id", "restaurant_id", "saturday", "sunday", "weekend_end", "weekend_start", "work_day_end", "work_day_start" },
                values: new object[] { new Guid("8c193535-8ac4-460b-8e77-3a3bd451e6d4"), new Guid("bce87730-d28c-4576-9c90-19d424f5ec5c"), true, true, new TimeSpan(0, 21, 30, 0, 0), new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "id", "name", "restaurant_id" },
                values: new object[] { new Guid("b7575dd8-93d5-44c8-a7b4-656a3c8f2f62"), "Pizza Menu", new Guid("bce87730-d28c-4576-9c90-19d424f5ec5c") });

            migrationBuilder.InsertData(
                table: "dishes",
                columns: new[] { "id", "description", "menu_id", "name", "picture_url", "price", "type" },
                values: new object[] { new Guid("56669f95-63d9-40e7-b548-d58337db2054"), "Pica sa šunkom i sirom.", new Guid("b7575dd8-93d5-44c8-a7b4-656a3c8f2f62"), "Capricciosa", null, 750.0, "Pizza" });
        }
    }
}
