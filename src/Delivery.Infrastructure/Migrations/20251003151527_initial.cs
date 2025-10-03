using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "allergens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_allergens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    profile_picture_url = table.Column<string>(type: "text", nullable: true),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "administrators",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_administrators", x => x.id);
                    table.ForeignKey(
                        name: "fk_administrators_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "couriers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    work_status = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_couriers", x => x.id);
                    table.ForeignKey(
                        name: "fk_couriers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                    table.ForeignKey(
                        name: "fk_customers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "owners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_owners", x => x.id);
                    table.ForeignKey(
                        name: "fk_owners_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    street_and_number = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    postal_code = table.Column<string>(type: "text", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_addresses_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "allergen_customer",
                columns: table => new
                {
                    allergens_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customers_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_allergen_customer", x => new { x.allergens_id, x.customers_id });
                    table.ForeignKey(
                        name: "fk_allergen_customer_allergens_allergens_id",
                        column: x => x.allergens_id,
                        principalTable: "allergens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_allergen_customer_customers_customers_id",
                        column: x => x.customers_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    base_work_sched_id = table.Column<Guid>(type: "uuid", nullable: true),
                    image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_restaurants", x => x.id);
                    table.ForeignKey(
                        name: "fk_restaurants_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_restaurants_owners_owner_id",
                        column: x => x.owner_id,
                        principalTable: "owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "menus",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    restaurant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menus", x => x.id);
                    table.ForeignKey(
                        name: "fk_menus_restaurants_restaurant_id",
                        column: x => x.restaurant_id,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_suspended = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    restaurant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workers", x => x.id);
                    table.ForeignKey(
                        name: "fk_workers_restaurants_restaurant_id",
                        column: x => x.restaurant_id,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_workers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dishes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    picture_url = table.Column<string>(type: "text", nullable: true),
                    menu_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dishes", x => x.id);
                    table.ForeignKey(
                        name: "fk_dishes_menus_menu_id",
                        column: x => x.menu_id,
                        principalTable: "menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_schedules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    week_day = table.Column<string>(type: "text", nullable: false),
                    work_start = table.Column<TimeSpan>(type: "time", nullable: false),
                    work_end = table.Column<TimeSpan>(type: "time", nullable: false),
                    courier_id = table.Column<Guid>(type: "uuid", nullable: true),
                    restaurant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fk_work_schedules_couriers_courier_id",
                        column: x => x.courier_id,
                        principalTable: "couriers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_work_schedules_restaurants_restaurant_id",
                        column: x => x.restaurant_id,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_work_schedules_workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "workers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "allergen_dish",
                columns: table => new
                {
                    allergens_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dishes_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_allergen_dish", x => new { x.allergens_id, x.dishes_id });
                    table.ForeignKey(
                        name: "fk_allergen_dish_allergens_allergens_id",
                        column: x => x.allergens_id,
                        principalTable: "allergens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_allergen_dish_dishes_dishes_id",
                        column: x => x.dishes_id,
                        principalTable: "dishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dish_option_groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    dish_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dish_option_groups", x => x.id);
                    table.ForeignKey(
                        name: "fk_dish_option_groups_dishes_dish_id",
                        column: x => x.dish_id,
                        principalTable: "dishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dish_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    dish_option_group_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dish_options", x => x.id);
                    table.ForeignKey(
                        name: "fk_dish_options_dish_option_groups_dish_option_group_id",
                        column: x => x.dish_option_group_id,
                        principalTable: "dish_option_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "af07b4c1-07e7-4f59-af11-bf6855cbafaa", "owner1@example.com", true, "Petar", "Petrovic", false, null, "OWNER1@EXAMPLE.COM", "OWNER1", "AQAAAAIAAYagAAAAELN/ad1vG+3DWCp5cQ0iCp/RUA1Hy5kqpYQ/HqEAOVqeXUeU2T8rEe7AReUzkg3a2g==", null, false, null, "3d304b05-0963-4e73-a7dd-e159a5b6cdfd", false, "owner1" },
                    { new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"), 0, "403ae674-c54b-44f2-bae7-52563deb2da9", "admin3@example.com", true, "Third", "Admin", false, null, "ADMIN3@EXAMPLE.COM", "ADMIN3", "AQAAAAIAAYagAAAAEHEo7h1qyRAgeY6VXEWlGYM0W1Dt3hg4RikComaUqrsSR9GhdTPKEm/HFyuwz9q6xQ==", null, false, null, "5d40d011-a16c-48fd-bf8a-451652aa5e89", false, "admin3" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 0, "c5a947a0-5261-4bbb-9dbd-e5e03e3ad01d", "customer1@example.com", true, "Marko", "Markovic", false, null, "CUSTOMER1@EXAMPLE.COM", "CUSTOMER1", "AQAAAAIAAYagAAAAEELUaRiFn1iKGJUUQeMumi8puSz8EQy/OW/eth1ZEQJJZv14+Cu8YNjngFcsxkgbuw==", null, false, null, "523ce08a-f4b5-4cea-a7cf-b7f115f1dff7", false, "customer1" },
                    { new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"), 0, "a3e4d34e-1d44-40d9-a965-f4ec5b1f9d18", "admin1@example.com", true, "Main", "Admin", false, null, "ADMIN1@EXAMPLE.COM", "ADMIN1", "AQAAAAIAAYagAAAAEEL7fGdXNbnjCY0jUjM1nEKnD1LOeMmGjAUDvIZX0D/Wz0N2iVkZtWrxkzHpummTOQ==", null, false, null, "c028b0a6-9584-4e86-a956-3e38474efa9f", false, "admin1" },
                    { new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"), 0, "5138cc34-8a69-4ce4-bad3-30a28bc08a28", "admin2@example.com", true, "Second", "Admin", false, null, "ADMIN2@EXAMPLE.COM", "ADMIN2", "AQAAAAIAAYagAAAAEGWER/W+ecdTTO7sUVr6s160RHesk8lWdx+KPMSkY3pmBcPcmDIjqmUMk6sxN+ou8g==", null, false, null, "3f9117ec-4dfd-4d27-b5d2-f9596797850b", false, "admin2" }
                });

            migrationBuilder.InsertData(
                table: "addresses",
                columns: new[] { "id", "city", "customer_id", "postal_code", "street_and_number" },
                values: new object[] { new Guid("b7ef45f8-0097-49ce-bac9-6a965cef38e0"), "Beograd", null, "11000", "Knez Mihailova 12" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("fc7e84f2-e37e-46e2-a222-a839d3e1a3bb"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334") },
                    { new Guid("5b00155d-77a2-438c-b18f-dc1cc8af5a43"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7") },
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f") }
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "id", "user_id" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.InsertData(
                table: "owners",
                columns: new[] { "id", "user_id" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "restaurants",
                columns: new[] { "id", "address_id", "base_work_sched_id", "description", "image", "name", "owner_id", "phone_number" },
                values: new object[] { new Guid("e28d7f8c-92b0-4775-a091-7a76147c888b"), new Guid("b7ef45f8-0097-49ce-bac9-6a965cef38e0"), new Guid("762c0f60-6c50-47c8-a4f7-6e93800df240"), "Autentična italijanska kuhinja.", "", "Pizzeria Roma", new Guid("33333333-3333-3333-3333-333333333333"), "222" });

            migrationBuilder.InsertData(
                table: "base_work_scheds",
                columns: new[] { "id", "restaurant_id", "saturday", "sunday", "weekend_end", "weekend_start", "work_day_end", "work_day_start" },
                values: new object[] { new Guid("762c0f60-6c50-47c8-a4f7-6e93800df240"), new Guid("e28d7f8c-92b0-4775-a091-7a76147c888b"), true, true, new TimeSpan(0, 21, 30, 0, 0), new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "id", "name", "restaurant_id" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), "Pizza Menu", new Guid("e28d7f8c-92b0-4775-a091-7a76147c888b") });

            migrationBuilder.InsertData(
                table: "dishes",
                columns: new[] { "id", "description", "menu_id", "name", "picture_url", "price", "type" },
                values: new object[] { new Guid("09b9d444-c2cf-48c6-a9ea-50f56434dadb"), "Pica sa šunkom i sirom.", new Guid("55555555-5555-5555-5555-555555555555"), "Capricciosa", null, 750.0, "Pizza" });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_customer_id",
                table: "addresses",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_administrators_user_id",
                table: "administrators",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_allergen_customer_customers_id",
                table: "allergen_customer",
                column: "customers_id");

            migrationBuilder.CreateIndex(
                name: "ix_allergen_dish_dishes_id",
                table: "allergen_dish",
                column: "dishes_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_base_work_scheds_restaurant_id",
                table: "base_work_scheds",
                column: "restaurant_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_couriers_user_id",
                table: "couriers",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_customers_user_id",
                table: "customers",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_dish_option_groups_dish_id",
                table: "dish_option_groups",
                column: "dish_id");

            migrationBuilder.CreateIndex(
                name: "ix_dish_options_dish_option_group_id",
                table: "dish_options",
                column: "dish_option_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_dishes_menu_id",
                table: "dishes",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "ix_menus_restaurant_id",
                table: "menus",
                column: "restaurant_id");

            migrationBuilder.CreateIndex(
                name: "ix_owners_user_id",
                table: "owners",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_restaurants_address_id",
                table: "restaurants",
                column: "address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_restaurants_owner_id",
                table: "restaurants",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_schedules_courier_id",
                table: "work_schedules",
                column: "courier_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_schedules_restaurant_id",
                table: "work_schedules",
                column: "restaurant_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_schedules_worker_id",
                table: "work_schedules",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_workers_restaurant_id",
                table: "workers",
                column: "restaurant_id");

            migrationBuilder.CreateIndex(
                name: "ix_workers_user_id",
                table: "workers",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrators");

            migrationBuilder.DropTable(
                name: "allergen_customer");

            migrationBuilder.DropTable(
                name: "allergen_dish");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "base_work_scheds");

            migrationBuilder.DropTable(
                name: "dish_options");

            migrationBuilder.DropTable(
                name: "work_schedules");

            migrationBuilder.DropTable(
                name: "allergens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "dish_option_groups");

            migrationBuilder.DropTable(
                name: "couriers");

            migrationBuilder.DropTable(
                name: "workers");

            migrationBuilder.DropTable(
                name: "dishes");

            migrationBuilder.DropTable(
                name: "menus");

            migrationBuilder.DropTable(
                name: "restaurants");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "owners");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
