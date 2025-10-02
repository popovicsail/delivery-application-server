using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRestaurantBaseWorkSched : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "allergen_dish",
                keyColumns: new[] { "allergens_id", "dishes_id" },
                keyValues: new object[] { new Guid("c2ee3eed-8a58-4bc2-abe6-391253b64a39"), new Guid("66666666-6666-6666-6666-666666666666") });

            migrationBuilder.DeleteData(
                table: "allergen_dish",
                keyColumns: new[] { "allergens_id", "dishes_id" },
                keyValues: new object[] { new Guid("c2ee3eed-8a58-4bc2-abe6-391253b64a39"), new Guid("77777777-7777-7777-7777-777777777777") });

            migrationBuilder.DeleteData(
                table: "allergen_dish",
                keyColumns: new[] { "allergens_id", "dishes_id" },
                keyValues: new object[] { new Guid("fcc53b83-25eb-4d96-9916-9ce034b6daea"), new Guid("66666666-6666-6666-6666-666666666666") });

            migrationBuilder.DeleteData(
                table: "allergen_dish",
                keyColumns: new[] { "allergens_id", "dishes_id" },
                keyValues: new object[] { new Guid("fcc53b83-25eb-4d96-9916-9ce034b6daea"), new Guid("77777777-7777-7777-7777-777777777777") });

            migrationBuilder.DeleteData(
                table: "dish_options",
                keyColumn: "id",
                keyValue: new Guid("ba4abc17-2b03-494e-a182-46a5def1b980"));

            migrationBuilder.DeleteData(
                table: "dish_options",
                keyColumn: "id",
                keyValue: new Guid("ecebb65d-12e9-47d4-94f8-ef033cbd8426"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("c2ee3eed-8a58-4bc2-abe6-391253b64a39"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("fcc53b83-25eb-4d96-9916-9ce034b6daea"));

            migrationBuilder.DeleteData(
                table: "dish_option_groups",
                keyColumn: "id",
                keyValue: new Guid("99f25578-4514-4903-8cf5-05abef38348e"));

            migrationBuilder.AddColumn<Guid>(
                name: "base_work_sched_id",
                table: "restaurants",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "restaurants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "base_work_scheds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    saturday = table.Column<bool>(type: "boolean", nullable: false),
                    sunday = table.Column<bool>(type: "boolean", nullable: false),
                    work_day_start = table.Column<TimeSpan>(type: "interval", nullable: false),
                    work_day_end = table.Column<TimeSpan>(type: "interval", nullable: false),
                    weekend_start = table.Column<TimeSpan>(type: "interval", nullable: true),
                    weekend_end = table.Column<TimeSpan>(type: "interval", nullable: true),
                    restaurant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_base_work_scheds", x => x.id);
                    table.ForeignKey(
                        name: "fk_base_work_scheds_restaurants_restaurant_id",
                        column: x => x.restaurant_id,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "4c7ff72a-aa89-4330-95f5-adaf3b78b7c4", "AQAAAAIAAYagAAAAEM8ChGAFZ4lTGs5wfQOIRlGBqZIo2HOdh4ex0RmQVmEDVj2BGLiLjzmvsV8eC0yFFg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "1c3779f2-100d-4ece-8963-eae08daf124e", "AQAAAAIAAYagAAAAEArwp7RMpMmpP2C25b4gewsIqrGt8s10Fx1Ak3FFBZrkQNmiQlqB5Xpzqcyy6/r4IQ==", "d6826e77-6db1-442e-abb7-b4697e7ed310" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "20cb8368-c77c-410a-97f5-add2e2249e43", "AQAAAAIAAYagAAAAEHO3WBYCTwad7HxXWX/zv/5nSTBj+Fq0KUE85nz+ynzw8zuhSORUZxDu3FWCYW5Qmg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "30a73fb8-999a-4286-a89e-f6f97cf2eb6d", "AQAAAAIAAYagAAAAEB+EfZyKZqAiVOH1HioorHiNC102ByZnZJF8VHkK0Y09bRVuiyBLT7fylUvcYT3rKQ==" });

            migrationBuilder.InsertData(
                table: "allergens",
                columns: new[] { "id", "name", "type" },
                values: new object[,]
                {
                    { new Guid("5acfa33c-1c6a-4f35-8fbb-7a095cdd8a6b"), "Gluten", "Cereals" },
                    { new Guid("a674c52b-af17-4097-af01-244b4c9b4762"), "Lactose", "Dairy" }
                });

            migrationBuilder.InsertData(
                table: "base_work_scheds",
                columns: new[] { "id", "restaurant_id", "saturday", "sunday", "weekend_end", "weekend_start", "work_day_end", "work_day_start" },
                values: new object[] { new Guid("9f7f967b-3940-45cf-92b2-09f33970cbbd"), new Guid("44444444-4444-4444-4444-444444444444"), true, true, new TimeSpan(0, 21, 30, 0, 0), new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "dish_option_groups",
                columns: new[] { "id", "dish_id", "name", "type" },
                values: new object[] { new Guid("559bb13f-2d52-4206-b34a-92807278504b"), new Guid("77777777-7777-7777-7777-777777777777"), "Extra Toppings", "Zavisni" });

            migrationBuilder.UpdateData(
                table: "restaurants",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "base_work_sched_id", "phone_number" },
                values: new object[] { new Guid("9f7f967b-3940-45cf-92b2-09f33970cbbd"), "065/555-333-1" });

            migrationBuilder.InsertData(
                table: "allergen_dish",
                columns: new[] { "allergens_id", "dishes_id" },
                values: new object[,]
                {
                    { new Guid("5acfa33c-1c6a-4f35-8fbb-7a095cdd8a6b"), new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("5acfa33c-1c6a-4f35-8fbb-7a095cdd8a6b"), new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("a674c52b-af17-4097-af01-244b4c9b4762"), new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("a674c52b-af17-4097-af01-244b4c9b4762"), new Guid("77777777-7777-7777-7777-777777777777") }
                });

            migrationBuilder.InsertData(
                table: "dish_options",
                columns: new[] { "id", "dish_option_group_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("5fe0a1fb-3564-44a5-a2e3-e2e12e59b133"), new Guid("559bb13f-2d52-4206-b34a-92807278504b"), "Extra Cheese", 120.0 },
                    { new Guid("7a207e5e-ee98-4904-b91a-8809fc7d814a"), new Guid("559bb13f-2d52-4206-b34a-92807278504b"), "Ketchup", 50.0 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_base_work_scheds_restaurant_id",
                table: "base_work_scheds",
                column: "restaurant_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "base_work_scheds");

            migrationBuilder.DeleteData(
                table: "allergen_dish",
                keyColumns: new[] { "allergens_id", "dishes_id" },
                keyValues: new object[] { new Guid("5acfa33c-1c6a-4f35-8fbb-7a095cdd8a6b"), new Guid("66666666-6666-6666-6666-666666666666") });

            migrationBuilder.DeleteData(
                table: "allergen_dish",
                keyColumns: new[] { "allergens_id", "dishes_id" },
                keyValues: new object[] { new Guid("5acfa33c-1c6a-4f35-8fbb-7a095cdd8a6b"), new Guid("77777777-7777-7777-7777-777777777777") });

            migrationBuilder.DeleteData(
                table: "allergen_dish",
                keyColumns: new[] { "allergens_id", "dishes_id" },
                keyValues: new object[] { new Guid("a674c52b-af17-4097-af01-244b4c9b4762"), new Guid("66666666-6666-6666-6666-666666666666") });

            migrationBuilder.DeleteData(
                table: "allergen_dish",
                keyColumns: new[] { "allergens_id", "dishes_id" },
                keyValues: new object[] { new Guid("a674c52b-af17-4097-af01-244b4c9b4762"), new Guid("77777777-7777-7777-7777-777777777777") });

            migrationBuilder.DeleteData(
                table: "dish_options",
                keyColumn: "id",
                keyValue: new Guid("5fe0a1fb-3564-44a5-a2e3-e2e12e59b133"));

            migrationBuilder.DeleteData(
                table: "dish_options",
                keyColumn: "id",
                keyValue: new Guid("7a207e5e-ee98-4904-b91a-8809fc7d814a"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("5acfa33c-1c6a-4f35-8fbb-7a095cdd8a6b"));

            migrationBuilder.DeleteData(
                table: "allergens",
                keyColumn: "id",
                keyValue: new Guid("a674c52b-af17-4097-af01-244b4c9b4762"));

            migrationBuilder.DeleteData(
                table: "dish_option_groups",
                keyColumn: "id",
                keyValue: new Guid("559bb13f-2d52-4206-b34a-92807278504b"));

            migrationBuilder.DropColumn(
                name: "base_work_sched_id",
                table: "restaurants");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "restaurants");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "b6867144-549b-4d8e-887e-39a366a08516", "AQAAAAIAAYagAAAAEJH789JImAdKB+gW0NrkGmdWlSIthj1BzS/vxjQXGBNd39GKRXLO2BExd1b3Cl8O0Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "d4f417be-a5f7-48ea-8722-e77a683839a3", "AQAAAAIAAYagAAAAEPVzhFRQDohVGeG3WQk79OG5pkdccoYPfgd8zcjC6Wkbw/d2C+wmV8Z+eMLKxOP+Jg==", "ce3a30da-2232-47ff-aecd-a97de952df33" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "171fdd30-6141-403b-b0cf-2e43a4b50f9d", "AQAAAAIAAYagAAAAED3vY84ClyIQ6l95QXoNN4k31BIsd3eXtVVBCx4dns9wq+JglXtX/XJCNHxnc33Zzg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"),
                columns: new[] { "concurrency_stamp", "password_hash" },
                values: new object[] { "05f85f84-7f03-4be8-bfb7-0595e0c82bc7", "AQAAAAIAAYagAAAAECKJA7PiQjXUYXEJdadv3qMwcRxeiOK9p4CzQGzY5tpNMW8q6ZXflsc945LXyVbhGQ==" });

            migrationBuilder.InsertData(
                table: "allergens",
                columns: new[] { "id", "name", "type" },
                values: new object[,]
                {
                    { new Guid("c2ee3eed-8a58-4bc2-abe6-391253b64a39"), "Lactose", "Dairy" },
                    { new Guid("fcc53b83-25eb-4d96-9916-9ce034b6daea"), "Gluten", "Cereals" }
                });

            migrationBuilder.InsertData(
                table: "dish_option_groups",
                columns: new[] { "id", "dish_id", "name", "type" },
                values: new object[] { new Guid("99f25578-4514-4903-8cf5-05abef38348e"), new Guid("77777777-7777-7777-7777-777777777777"), "Extra Toppings", "Zavisni" });

            migrationBuilder.InsertData(
                table: "allergen_dish",
                columns: new[] { "allergens_id", "dishes_id" },
                values: new object[,]
                {
                    { new Guid("c2ee3eed-8a58-4bc2-abe6-391253b64a39"), new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("c2ee3eed-8a58-4bc2-abe6-391253b64a39"), new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("fcc53b83-25eb-4d96-9916-9ce034b6daea"), new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("fcc53b83-25eb-4d96-9916-9ce034b6daea"), new Guid("77777777-7777-7777-7777-777777777777") }
                });

            migrationBuilder.InsertData(
                table: "dish_options",
                columns: new[] { "id", "dish_option_group_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("ba4abc17-2b03-494e-a182-46a5def1b980"), new Guid("99f25578-4514-4903-8cf5-05abef38348e"), "Ketchup", 50.0 },
                    { new Guid("ecebb65d-12e9-47d4-94f8-ef033cbd8426"), new Guid("99f25578-4514-4903-8cf5-05abef38348e"), "Extra Cheese", 120.0 }
                });
        }
    }
}
