using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                name: "vouchers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    date_issued = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    discount_amount = table.Column<double>(type: "double precision", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vouchers", x => x.id);
                    table.ForeignKey(
                        name: "fk_vouchers_customers_customer_id",
                        column: x => x.customer_id,
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
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "date_of_birth", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "profile_picture_url", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "902e1bb2-9a8e-4508-a391-3732623531fa", null, "owner1@example.com", true, "Petar", "Petrovic", false, null, "OWNER1@EXAMPLE.COM", "OWNER1", "AQAAAAIAAYagAAAAECutqhfwJr6b5h+488MnTFWkKeg8jLumlPbf+HiUKYyCpFOobtQuM5sV/oM6a9l9+g==", null, false, "data:image/png;base64,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", "a244ef05-8da7-464e-8e49-c05b64407dbb", false, "owner1" },
                    { new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"), 0, "5508cc08-98bd-423e-b0de-6c99d43994b6", null, "admin3@example.com", true, "Third", "Admin", false, null, "ADMIN3@EXAMPLE.COM", "ADMIN3", "AQAAAAIAAYagAAAAEGb8Cq9Oya/sLmVxFyG8+Wz/KLKn8QV3HmFvaRtUxQWJEDmJhk2GHcc6c+vhXWQbPw==", null, false, "data:image/png;base64,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", "135b3047-3df9-41d0-94e1-76ca99c422ce", false, "admin3" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 0, "88941d48-5def-4186-8335-b258f674e4d7", new DateTime(2025, 10, 19, 20, 38, 8, 286, DateTimeKind.Utc).AddTicks(6710), "customer1@example.com", true, "Marko", "Markovic", false, null, "CUSTOMER1@EXAMPLE.COM", "CUSTOMER1", "AQAAAAIAAYagAAAAECEh5j+itm6iDM73L3ix9sdPj8bF8OFwbvcCVU3VlrcLGBEO1BA4MorYEE83W877cA==", null, false, "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAABkAAAAZACAMAAAAW0n6VAAAClFBMVEU3S2A4S2E5TGI6TWI6TWM7TmM8T2Q9T2U+UGU+UWY/UWdAUmdBU2hBVGhCVGlDVWpEVmpEVmtFV2xGWGxHWG1HWW1IWm5JWm9KW29KXHBLXXFMXXFNXnJNX3JOX3NPYHRQYXRQYXVRYnZSY3ZTZHdUZXhVZnlWZnlWZ3pXaHtYaHtZaXxaanxaan1ba35cbH5dbX9dbYBeboBfb4Fgb4FgcIJhcYNicYNjcoRjc4Rkc4VldIZmdYZmdodndohod4hpeIlqeYpreotseotse4xtfI1ufI1vfY5vfo5wf49xf5BygJBygZFzgZJ0gpJ1g5N2g5N2hJR3hZV4hpV5hpZ5h5d6iJd7iJh8iZh8ipl9ipp+i5p/jJt/jJyAjZyBjp2Cj52Cj56DkJ+EkZ+FkaCFkqGGk6GHk6KIlKKIlaOJlaSKlqSLl6WLmKaMmKaNmaeOmqePmqiPm6mQnKmRnKqSnauSnquTn6yUn6yVoK2Voa6Woa6Xoq+Yo7CZpLGapbGbpbKbprOcp7OdqLSeqLWeqbWfqragqrahq7ehrLiirLijrbmkrrmkrrqlr7umsLunsbynsb2osr2ps76qs76rtL+rtcCstcCttsGut8KvuMOwucOxusSxusWyu8WzvMa0vMe0vce1vsi2vsi3v8m3wMq4wcq5wcu6wsy6w8y7w828xM29xc69xc++xs+/x9DAx9HAyNHBydLCytLDytPDy9TEzNTFzNXGzdbHztbHztfIz9fJ0NjK0NnK0dnL0trM09vN09vN1NzO1dzP1d3Q1t7Q197R19/S2ODT2eDT2uHU2uHV2+LW3OPX3eTY3uXZ3uXZ3+ba4Obb4Ofc4ejc4ujd4+ne4+rf5Orf5evg5evh5uzMg2v8AAAgAElEQVR42u3d558X5bnAYZbdRXAFKbbFSlHEgiVgLERREgvGgsaIJQZNjqhRrFhBY1CToJGgKWAjNoxGNEZiQ1FBLCtlZWGb/8xJOcmxIOzO/maeeZ65rnfn5cne9/39OMvO9PscADLo538CAAQEAAEBQEAAEBAAEBAABAQAAQFAQAAQEAAQEAAEBAABAUBAABAQABAQAAQEAAEBQEAAEBAAEBAABAQAAQFAQAAQEAAQEAAEBAABAUBAABAQABAQAAQEAAEBQEAAEBAAEBAABAQAAQFAQABAQAAQEAAEBAABAUBAAEBAABAQAAQEAAEBQEAAQEAAEBAABAQAAQFAQABAQAAQEAAEBAABAUBAAEBAABAQAAQEAAEBQEAAQEAAEBAABAQAAQFAQABAQAAQEAAEBAABAUBAAEBAABAQAAQEAAEBAAEBQEAAEBAABAQAAQEAAQFAQAAQEAAEBAABAQABAUBAABAQAAQEAAEBAAEBQEAAEBAABAQAAQEAAQFAQAAQEAAEBAABAQABAUBAABAQAAQEAAEBAAEBQEAAEBAABAQABAQAAQFAQAAQEAAEBAAEBAABAUBAABAQAAQEAAQEAAEBQEAAEBAABAQABAQAAQFAQAAQEAAEBAAEBAABAUBAABAQAAQEAAQEAAEBQEAAEBAABAQABAQAAQFAQAAQEAAEBAAEBAABAUBAABAQABAQAAQEAAEBQEAAEBAAEBAABAQAAQFAQAAQEAAQEAAEBAABAUBAABAQABAQAAQEAAEBQEAAEBAAEBAABAQAAQFAQAAQEAAQEAAEBAABAUBAABAQABAQAAQEAAEBQEAAQEAAEBAABAQAAQFAQABAQAAQEAAEBAABAUBAAEBAABAQAAQEAAEBQEAAQEAAEBAABAQAAQFAQABAQAAQEAAEBAABAUBAAEBAABAQAAQEAAEBQEAAQEAAEBAABAQAAQFAQABAQAAQEAAEBAABAQABAUBAABAQAAQEAAEBAAEBQEAAEBAABAQAAQEAAQFAQAAQEAAEBAABAQABAUBAABAQAAQEAAEBAAEBQEAAEBAABAQAAQEAAYHt2/D+q88uXnDnnCsvm3HGaZOPO/yQsfvs07zzP/T7j4Z//B/D99ln1CETjpt82lkzLp998/xFS196Z123//EQEKiYlr8/uXDe1ReefMzB++xc1y+7IXsffOy0S66bv/iF1e3+V0VAIFltbzy54IaLTprQ3NgvB7uMm3L+9QueXqkkCAgko+OdJ+76yakTdulXiP7Nk6bPfvCljf53R0Ag6nLMu+SEUQ39Qtj1qPNvXfxWh58CAgJR2fzKwqtOGR2mHF/SMOa02b97q9NPBAGB8nv3j9efNqa+X6kMOmzGL57f5GeDgEBJdb72wE++PaRfWTWMO+fOF7b4MSEgUC7v/PaySU39yq9hwiUPvOPHhYBAKbQ+c8PU4f1isuv3bnvRb9cREAjqo9/NPLShX4x2PO66ZZ5nISAQRMvDF43pF7UdJt/8SpcfJAICRdq45NID+/dLwfDT71vt54mAQCE6ll15WEO/lIy++LHNfq4ICOTro19PG9IvQYOm3vuRny4CAjnpeuHqCXX9klU34bpX/JAREKi5zYvPGd4vec0/Xua36ggI1NDGRac19auI3S/REAQEaqPlvikD+lXKrhc/6/WLCAj00YYFk+v7VdDul/7NDx8Bgcy2/OGUgf0qa9zNa40AAgIZdD71gyH9qq3/cQtaDQICAr2zctbu/ejXb8dznjcMCAj02Ge/nlSnHf+x/7x1RgIBgZ54YcZg1fiSgWctMxYICGzHhrnjBGMrxszdYDgQEPhmK2Y0acU3aLr4LQOCgMBWdSyaJBPbfFnWdx7zN+oICHzNh7N3k4jtGjVvo1FBQOBLz66mN6pDjwyZ5bXvCAj815++41/t9twOM942MggI/EPHA+NFoZd/oX7qS+YGAaHyPrujWRAyOOZJs4OAUGmtc0ZoQUYT/2R+EBAqa+P1w3RAQhAQ6K31s3fWAAlBQKDX+bh6J/e/FgnxliwEhGppu8XDq1qZssI8ISBURsd83/qo5T/qPfs9M4WAUAldC/dz9GurcWaLuUJASN/jBzr4tdd0Q5vRQkBI2+vHO/b5GLmw23ghIKSr5aIGlz43h//VhCEgJKr9tiGufK6/Tf/Bh6YMASFFS/zuPP9fhdzYbtAQEFLz3lTnvQijvWQRASGxp1fXD3TbC3K651gICAlZOspdL85OczuMHAJCGtac5qgXa/xfTB0CQgK67/bWxOL/PdYlrSYPASF2Kyc55yHs9YTZQ0CIWsccvzwPZfo684eAEK+/jnfHw9nlIROIgBCp9iu9uCSskz8xhQgIMXplnAse2ojfm0MEhOh0XOc/P8rgzPVmEQEhLm8f7naXQ7N3myAgRGX+ji53WdRdutlAIiDEYv0pznaZjHvDTCIgxOHPI93schl0r6lEQIhA1w1+e14+0/wuHQGh9D442rUuoz2fN5sICOX2yHC3upwabugynggI5dX5szqXurQmf2pCERDK6iOPr0ptpM+EICCU1PN7uNElf4w1z5QiIJTRzxtd6NL7vg9NISCUzpbprnMUf1T4jllFQCiXj45wm+MwdKlpRUAok5ebXeZY1N9uXhEQymORD9fG5GxvV0RAKImuK/31R1wmfGBqERDKoO17LnJsdltubhEQwvtkgnscnx3/YHIREEJ7cx/XOEb9bzG7CAhhLRvqFkfq/A7ji4AQ0IMDHOJoTd5ggBEQgrnFP7+K2QFrjDACQhjd/+MGx635dVOMgBBC57kucOyGPmeOERCKt3mq+xu/gYtNMgJC0TYe5fqmoH6+WUZAKFbLQW5vIm4wzQgIRfr4AIc3GZd3G2gEhMKsHePsJuSCLiONgFCQ9/d1dJNypj9KR0Aoxqo9ndzETN1irBEQCrByDwc3OZM3GWwEhNy97eu1KTr6M6ONgJCzd/QjTZNaDTcCQq7e149UfUtBEBDytGY/hzZZExUEASE/Lf7+I+mnWH6TjoCQlw3jHNm0f5OuIAgI+dh0uBOb+r/m9fcgCAh5aD/OgU3eSe0GHQGh5jpPd14rYFqnUUdAqLULHddK+IF38yIg1Ngcp7UiLjXsCAg1dU+dy1oVNxl3BIQaWlzvrlbH3QYeAaFmXh7kqlZI/R+MPAJCjazxAqxqaXzK0CMg1IQ/QK+cwSuMPQJCDXROcVArp3mNwUdA6LuZzmkFjdtg8hEQ+mq+Y1pJx3ipCQJCHz3V6JZW0w8NPwJCn7w9zCWtqjnGHwGhD1rHuqOVVfdbC4CAkFn3Kc5ohQ160QogIGR1kyPqH/OCgJDBE96AVXGH+sYtAkImq4a6oFV3mq+DICBk0Dbe/WS2RUBA6L3zXU/61S2xCQgIvfWA48k/DF5pFxAQeueNHd1O/mnsRtuAgNAbm/wFIf/nZL9IR0DojenuJv9xg31AQOg5vwDh/9U/aSMQEHpqVZOryf8bsdZOICD0TMdhbiZfNKnTViAg9MiVLiZfNstWICD0xDNegcVX+HtCBISeWN/sXvJVQ9+3GQgI23WWa8nXTfRrEASE7fm9W8nWXG03EBC27ePhTiVbU/+s7UBA2KapLiVb17zOeiAgbMMCd5JvMtV+ICB8s7VDnEm+0XwbgoDgARZZNL1tRRAQvsEiN5JtOcK/5UVA2LqWEU4k23S9LUFA2Cp/Qsh2NLxsTRAQtuJx95HtGbvZoiAgfM2mvZ1Htutym4KA8DWzHEe2r/4Fq4KA8BWvNTiO9MAYD7EQEL6se6LTiIdYCAgZ/NJhpIcPsZZbFwSEL/h0mMNID+3fbmEQEP7fhc4iPXadhUFA+K+/+Qw6PTfgLSuDgPB/uic5ivTCpG5Lg4Dwbw86ifTKPZYGAeFfPtvDRaRXdv7I2iAg/NNsB5FeOsPaICD8w9pB7iG9VPe0xUFA+Pzzc51Dem1sh81BQPhbf9eQ3ptjdRAQjnULyaBptd1BQKruUaeQTE6zPAhIxXUd6BKSzTLrg4BUm78hJKtDuuwPAlJlHfu6g2T1awuEgFTZ3a4gme3xmQ0SEP8TVNem3VxBsrvGCgmI/wmq62Y3kD4YtMYOCQhV1TrcDaQvzrJEAkJVzXEB6ZM630cXEKr6HyA+hE4ffcunpQSEarrR/aOvFtkjAaGS/wEy1Pmjr/baYpMEBL8BgSzm2iQBoXradnX86LsRrXZJQKicu9w+auFauyQgVE3Hnk4ftbBTi20SECrmVy4ftXGZbRIQqqV7rMNHbQxca58EhEpZ4u5RKz+2TwJCpXzb2cN/giAgZPCSq0ftXGKjBIQKmeboUTs7fGClBITKWN3g6FFDP7JTAkJlzHLyqOlvQfwtiIBQFVt8SIrauspWCQgVscDBo7aGeCOWgFARhzt41Nit1kpAqISXnTtqbfd2iyUgVMG5zh01d5/FEhAq4NOBrh01N6rLagkI6bvVsSMHv7NaAkLyuvZ168jBBLslICTvUaeOXDxluQSE1E1x6cjFdyyXgJC4tV6DRT7qVlovASFtcxw6cuLDUgJC2rpHuXPkxPtMBIS0PefMkZu7LZiAkDJ/hU5+xnbbMAEhXZuaXDny84wVExDS9Vs3jhydbMUEhHRNcePIUcNqOyYgpOoTfwRCrq60ZAJCqua5cORq+BZbJiAkyqcIydlCWyYgpOm9OgeOfB1rzQSENN3svpGzunftmYCQpEPcN/J2lT0TEFK00nUjd3t02DQBIUE3uW7k7zGbJiAkaILjRv5Ot2kCQnrW+DdYFGDgRrsmICRnrttGEe61awJCcr7ttFGESXZNQEjNunqnjSLUvW/bBITEPOiyUYybbZuAkJjpDhvFGG/bBIS0dI1w2CjIW/ZNQEjKi84aRbnGvgkISZntrFGUsfZNQEjKYc4ahXndwgkICfmkv6tGYa6zcQJCQhY4avh3WAgIWXzfUaNAb1s5ASEZXcPdNAp0m50TEJLxqpNGkbwPS0BIxx1OGkWqX2fpBIRUnOikUaj7LZ2AkIjOnVw0CnWarRMQEvGSg0axBndaOwEhDT5GSNGes3YCQhpOds8o2JXWTkBIg1e5U7SDrZ2AkISVzhlFq2uxeAJCCn7jnFG4RRZPQEjBTNeMws2weAJCCo5wzSjcvhZPQEhAx0DXjOK9b/UEhPj91S0jgAVWT0CI33y3jADOt3oCQvzOc8sIYIzVExDid6BbRgD+EkRAiN+WereMEP5g+QSE2K1wyQjiCssnIMTuQZeMII6xfAJC7K5yyQiiyTdBBITYfdclI4wVtk9AiNwoh4ww/CmhgBC5Nv8Ii0BmWj8BIW5/c8cI5CjrJyDEzcdACGWw9RMQ4natO0Yoq+2fgBC1s50xQnnc/gkIUfuWM0Yot9k/ASFquzljhPID+ycgxKy9zhkjlEkWUECI2TuuGMHsbgEFhJg944oRTqsNFBAi5s9ACMjbsASEmN3uiBHOUhsoIETsp44Y4fzaBgoIEZvuiBHOjTZQQIjYFEeMcC6xgQJCxA53xAjnZBsoIERstCNGOIfbQAEhYns4YoSzjw0UECI22BEjnOE2UECIWJMjRjiNNlBAiPnnCwF1WEEBIVpdThghbbCDAkK0NjhhCAgCgoAgIAgIAoKAICAICAiIgCAgICAICP+2yQlDQBAQsv18IaAtVlBAEBDIwgYKCBFrcMMIp84GCggR8zJFAhpsAwWEiO3iiBHOMBsoIERspCNGOCNtoIAQsbGOGOGMsoECQsTGO2KEM84GCggRO8oRI5yJNlBAiNhUR4xwTrCBAkLEpjtihDPNBgoIEbvEESOc82yggBCxqx0xwplpAwWEiN3miBHO1TZQQIjYvY4Y4dxhAwWEiD3kiBHOr22ggBCxpY4Y4Sy2gQJCxJY7YoSzzAYKCBF7yxEjnNdtoIAQsY8dMcL50AYKCBHrcMQIp90GCggxG+qKEcoQ+ycgRG2UM0YoPgciIMRtkjNGKEfaPwEhaic7Y4TyPfsnIERthjNGKD+0fwJC1GY7Y4TiXYoCQtzmO2OEcpf9ExCittgZI5Tf2z8BIWpehkUwf7F/AkLU1jhjhPK+/RMQotZR544RiDeZCAiRG+GOEcZw2ycgRO5gh4wwxts+ASFyUx0ywjjR9gkIkfuRQ0YYF9g+ASFycxwywrjJ9gkIkfuNQ0YY99s+ASFyzzpkhPG07RMQIveeQ0YYq2yfgBC5jgaXjBDqO2yfgBC7kU4ZITTbPQEhej5qSxAT7Z6AEL3pThkhnGX3BIToXe2UEcKVdk9AiN4vnTJCuMfuCQjR84cgBOHPQASE+PmkFEH4nJSAEL+uAW4ZxWvstHsCQvzGOmYUb5TNExASMMUxo3jH2zwBIQG+CEIAF9k8ASEB8xwzijfX5gkICXjUMaN4S2yegJCAtx0ziveWzRMQEuCF7hSvwcvcBYQkjHbOKNp+9k5ASMIU54yi+Ve8AkIaLnPOKNpMeycgJOEe54yi/cLeCQhJeM45o2jL7J2AkIRPnTOK9om9ExDSMNw9o1hDbZ2AkIhJDhrF+patExASMcNBo1g/tHUCQiLucNAo1m22TkBIxGMOGsV6xNYJCIl430GjWKtsnYCQiO5BLhpFGthl6wSEVBzkpFGkcXZOQEjGGU4aRTrdzgkIyZjjpFGkG+ycgJCMR5w0irTYzgkIyVjtpFGkd+2cgJCOwW4axWnqtnICQjomOmoU5wgbJyAk5EJHjeLMsHECQkLudtQozp02TkBIiI8SUqBnbZyAkJD1jhrFWWfjBISU7OGqUZQ97JuAkJTjnTWKcrx9ExCScrmzRlH+x74JCEn5rbNGURbaNwEhKW86axTlDfsmICSla0d3jWIM6rRvAkJajnDYKMZhtk1ASMxFDhvF8CITASE19zpsFGO+bRMQEvOSw0Yxlts2ASExmxtcNopQ32bbBITUHOC0UYSxdk1ASM50p40inGnXBITk3OG0UYTb7JqAkJxnnTaK8LRdExCS09rfbSN//TfaNQEhPWMcN/I32qYJCAk623Ejf2fYNAEhQXMdN/J3h00TEBL0Z8eN/C2zaQJCgjbVu27krb7VpgkIKRrnvJE3f4cuIKTpHOeNvE23ZwJCku503sjbPHsmICRpufNG3v5izwSEJG1udN/IV+NmeyYgpGmCA0e+DrVlAkKifuTAka+LbZmAkKgHHDjydb8tExAStdKBI19v2TIBIVHdQ1048jS025YJCKk6wYkjT5PtmICQrGucOPJ0jR0TEJL1mBNHnh6xYwJCstbVuXHkqMWOCQjpGu3GkZ9RNkxASJgX8pKjc2yYgJCw+Y4c+ZlvwwSEhL3myJGfV22YgJCwrsGuHHkZ3GXDBISUTXbmyIs/IxQQ0natM0de/BmhgJC2pc4ceXnCfgkISdtQ786Rj/4b7JeAkLbxDh35ONB2CQiJ81VCcnKR7RIQEvdbh458LLRdAkLi1jh05GON7RIQUreXS0ceRtotASF5Zzl15OEMuyUgJO8XTh15uNtuCQjJe92pIw+v2S0BIXndI9w6am94t90SENJ3qmNH7Z1sswSECpjn2FF782yWgFABKxw7as/HpASEKuga5tpRazv7mJSAUAlTnTtqbaq9EhAq4Q7njlq7zV4JCJXwV+eOWnvZXgkIldA52L2jtnbqtFcCQjVMcfCorSm2SkCoiNsdPGrrVlslIFTEqw4etfWKrRIQKsJfglBbw/wViIBQFWsEhJra+W1bJSBUQ9t4F4/a2m+9vRIQKuF8945aO95DLAGhCn7n2lF7N9ksASF9H/oFCDloeMFuCQjJO8mtIw+j2iyXgJC4RS4d+bjMdgkIadu4m0NHPupftF8CQtJ+4s6Rl0P9SywBIWUrG5w5cnOvDRMQEuY36ORo2DorJiAk62k3jjz9jx0TEJJ1pBNHngZ+YMkEhEQ95sKRrxm2TEBIU/chDhz5alhpzwSEJC1x38jb+fZMQEjSJOeNvDV+aNEEhAT9xXXDP8RCQMjiFMeN/O3k01ICQnre7u+4UYBb7ZqAkJyZThtF2McbsQSE1LQNddooxFLbJiAk5gGHjWKcbNsEhMRMdNgoRsNa6yYgJOV1d42iXG/fBISkXOqsUZTR9k1ASEnX7s4ahXnZxgkICXnGUaM4/hpdQEjJBY4axWnutnICQjI6hjtqFOg5OycgJOMJJ40iXWznBIRknOukUaSRdk5ASEXXCCeNQq2wdQJCIl500CjWHFsnICTiOgeNYh1l6wSERBzmoFGshg3WTkBIwic+JUXRHrJ3AkISHnTOKNq59k5ASMKZzhlF29veCQhJ8CJFirfa4gkICXjbMaN499s8ASEB9zpmFO+HNk9ASMA5jhnF29/mCQgJGO2YUbz+rVZPQIjeujrHjACesnsCQvQec8oI4Sa7JyBEb7ZTRgin2T0BIXpTnTJC2M/uCQjRa3bKCKFuo+UTECLX4pIRhg+jCwix+5NDRhh32z4BIXJzHTLCuMj2CQiRm+GQEcbRtk9AiNy3HDLC2MP2CQiRG+KQEchn1k9AiNo6Z4xQVtg/ASFqLzpjhPIH+ycgRO03zhih3GL/BISoXeeMEcpF9k9AiNq5zhihnGT/BISoHeeMEcp4+ycgRG2UM0Yow+2fgBC1gc4YwWy2gAJCxLyLl4DesYECQsRed8QI50UbKCBE7ClHjHAW20ABIWKLHDHCuc8GCggR8zUQArrBBgoIEbvaESOcn9hAASFilzhihHOeDRQQInaWI0Y4p9hAASFiJzpihHOcDRQQIuaDtgR0qA0UECI2zhEjnLE2UECI2D6OGOGMtIECQsSaHTHC8TpeASFmgx0xwtnJBgoIEWt0xAjIBgoIMf98QUAQEASE2LRaQQEhWhudMELaYAcFhGhtcMIQEAQEAUFAEBAEBAFBQBAQEBABQUBAQBAQBAQBQUAQEAQEAaFoHU4YIbXZQQEh4p8vBGQDBYSINblhhDPABgoIEfM6dwIabAMFhIj5oBQB7WIDBYSIjXbECGc/GyggROwQR4xwDrKBAkLEvu2IEc4kGyggRGyqI0Y4J9pAASFi0x0xwpluAwWEiF3qiBHOTBsoIETsOkeMcK61gQJCxH7uiBHOXTZQQIjY7xwxwnnYBgoIEXvOESOcZTZQQIjYSkeMcFbaQAEhYq2OGOFstIECQsx2csUIpcn+CQhRG+uMEcpY+ycgRO14Z4xQJts/ASFqP3TGCOU8+ycgRO1GZ4xQbrJ/AkLUfuuMEYq/IxQQ4vaiM0YoK+yfgBC1FmeMUFrtn4AQt53dMcLYw/YJCJE70iEjjKNsn4AQuXMcMsI43/YJCJGb45ARxi22T0CI3CMOGWEstX0CQuRWO2SE8YntExBiN9glI4Td7Z6AEL1JThkhnGD3BITozXTKCOEKuycgRG+BU0YIi+yegBC915wyQlhp9wSE6HU2uWUUb2i33RMQ4neMY0bxTrR5AkICrnTMKN6NNk9ASMBix4ziLbN5AkICPq1zzShawyabJyCk4ADnjKJNsHcCQhIucM4o2kx7JyAkYZFzRtGW2DsBIY1fgvR3zyj4VyC+hy4gJOJQB41iTbJ1AkIirnDQKNYNtk5ASMRTDhrFWm7rBIREbBnkolGkoZ22TkBIxfFOGkWaZucEhGT83EmjSAvtnICQjA+8zYQCNW6wcwJCOiY4ahTnOzZOQEjIjY4axZlv4wSEhLzuqFGY/h/aOAEhJaOcNYpyhH0TEJIyy1mjKHPtm4CQlBXOGgVp+MS+CQhpGe+wUYzjbZuAkJhbHDaK8aBtExASs8ZHQShE02e2TUBIzVFOG0U4264JCMm5z2mjCE/ZNQEhOa1Nbhv5G9Vt1wSE9FzguJG/222agJCgVxw3crdDi00TEFLklbzk7kx7JiAk6R7njbw9Z88EhCS1DnHfyNdB1kxASNRPHTjytcCWCQiJWt3gwpGnke22TEBI1TQnjjzdascEhGT9xYkjR03r7ZiAkK7DHTnyM9OGCQgJW+zIkZuBa22YgJCw7nHOHHn5kQUTEJL2kDNHTgZ8YL8EhKR1jXHoyMeF1ktASNxvHDry+Q+QNbZLQEhcx2inDv8ECwEhi4edOnKwk/e4Cwjp6z7UsaP2brRaAkIFLHXsqLndNtksAaEKjnXuqLV77JWAUAkv93fvqK3xnfZKQKiGcxw8aqpuma0SECrio52cPGrpDEslIFTGHCePGmryEhMBoTq27OvoUTtzrJSAUCFLHD1qZlyHjRIQquRkZ48aqX/RPgkIlfJBk8NHbVxinQSEipnn8FETza22SUComE6vxKIW6p6wTAJC5axodPzouwuskoBQQdc7fvTZvh5gCQhV1HGI80df/wWWd5gICB5iQRZXWCMBoaJudwDpk8P9CaGAUFXdU5xA+mDIe5ZIQKislmZHkOwWWSEBocKW1buCZPUjCyQgVNq1ziAZHdlufwSESuv8tkNIJiPWWB8B8T9Bxa0d7hSSQcMzlgcBqbzH+juG9N7dVgcBwStNyOBCi4OA8Pnn3ac4h/TSMf6CEAHhnz4b5yDSK+M2WBsEhH9Z5Rfp9Eazf4CFgPAfLw5yFOmxwSusDALCf/22zlmkhwY9bWEQEL5gjrtIzzQ+al0QEL7kMpeRnqh/2LIgIHxZ95luI9tXt8CuICB8VfsU15Ht9uPnNgUB4es2HeM+sp1+zLMnCAhb03qEC8k2f/9xny1BQNi6DQrCNjQstCMICApC7+2wxIYgIHyzjRPdSbau6Sn7gYCwLZsmu5RsTbP3lyAgbMeW09xKvu7gtXYDAWF7uma6lnzVSa02AwGhB271ZkW+pO7KTmuBgNAjCwe4mfy/nf5oJxAQeuqFEa4m//31x0obgYDQc2sOdTf5l/4/bbcPCAi9sXmG08k/zbAMCAi99WCT40m/fkutAgJCr13teNJvsAdYCAi9t8L1pN/pFgEBIYMxzmzP0SMAAByBSURBVCeL7AECgmdYZDBgoz1AQPAMiwyOtwYICJ5hkcV8W4CA4BkWGfT3Dl4EBM+wyOIwS4CA4BkWWcyxAwgInmGRxZt2AAHBMywyGGMFEBA8wyKLWTYAAcEzLLL4iw1AQPAMiwx267IBCAieYZGBT4EgIHiGRSaPmX8EBM+wyKBpi/lHQPAMiwymmX4EBM+wyGKh6UdA8AyLDBo2mH4EBM+wyGCy2UdA8AyLLH5u9hEQ+ujvTmkl1X1g9hEQPMMigwkmHwHBMyyyuNHkIyB4hkUWr5t8BATPsMhglLlHQPAMiyx+au4REDzDIos/m3sEBM+wyGCET4EgIHiGRRY/NPUICJ5hkcUSU4+A4BkWGfgUCAKCZ1hkcqqZR0DwDIssHjDzCAieYZFBw3ojj4BQK9c4qlVyrIlHQPAMiyzmmXgEBM+wyKBujYFHQPAMiwwOMe8ICJ5hkcV15h0BwTMsslhh3BEQPMMig31NOwKCZ1hkcZlpR0DwDIsslhl2BATPsMhgeKdhR0DwDIsMfmDWERA8wyKLxUYdAcEzLDLYsc2oIyB4hkUG3zPpCAieYZHFAoOOgFB7s13X9NWvM+gICJ5hkcHR5hwBwTMssvApEAQEz7DI5H1jjoDgGRYZHGTKERA8wyKL2YYcAcEzLLL4myFHQPAMiwz2MuMICJ5hkcVMI46A4BkWWTxjxBEQPMMig2E+BYKA4BkWWUw34AgInmGRxR8NOAKCZ1hkMHCTAUdAyNFYdzZZU403AoJnWGTxK+ONgOAZFhnUtxhvBATPsMjgKMONgOAZFlncYbgREDzDIotVhhsBwTMsMhhntBEQPMMii6uNNgKCZ1hk8VejjYDgGRYZjOw22QgInmGRwY8NNgKCZ1hk8ZTBRkDwDIsMhnaYawQEz7DI4GxjjYDgGRZZ/M5YIyB4hkUGO7SaagQEz7DI4ERDjYDgGRZZ3GeoERA8wyKD/p+YaQQEz7DIYKKRRkDwDIssbjPSCAieYZHF2yYaAcEzLDLY30AjIHiGRRZXGWgEBM+wyOIl84yA4BkWGTT7FAgCQpFec3eTcbFxRkDwDIsslppmBATPsMhgsE+BICB4hkUWZxhmBATPsMjiIbOMgOAZFhkM8CkQBATPsMhiilFGQPAMiyzuMckICJ5hkUH/j0wyAoJnWGRwhEFGQPAMiyxuNscICJ5hkcVb5hgBwTMsMhhrjBEQPMMiiytMMQKCZ1hk8aIpRkAI4nUHOHK7+xQIAoJnWGRxgRlGQPAMiyyeMMMICJ5hkcHgLWYYAcEzLDKYZoIREDzDIotFJhgBwTMsMhiwwQQjIHiGRQbfMb8ICJ5hkcUvzC8CgmdYZFC31vwiIHiGRQaHmV4EBM+wyOIm04uA4BkWWbxpehEQPMMig9FmFwHBMyyyuNzsIiB4hkUWL5hdBATPsMhg1y6ji4DgGRYZnG9yERA8wyKLR00uAoJnWGTQ5FMgCAieYZHFaeYWAcEzLLJ40NwiIHiGRQYNPgWCgOAZFllMNrUICJ5hkcVdphYBwTMsMqhbY2gREDzDIoMJZhYBwTMssrjBzCIglMX+bnJUXjOyCAieYZHBfiYWAcEzLLKYY2IREDzDIoNdNhlYBATPsMhgnnlFQPAMiwxGehEvAoJnWGRxr2lFQPAMiwxGdZhWBATPsMjAi9wREDzDIosDuswqAoJnWGTwR6OKgFA2b7jNMZjQbVQREErnBNc5AksNKgJC+Syvc55L7yhzioBQRie5z6X3nDFFQCijv/pPkLI73pQiIJTTd13ocqt72ZAiIJTTCv8JUm4nm1EEhLI61Y0us/4+RIiAUFqv9XelS+wsE4qAUF6nu9Ll1fCOAUVAKK836t3p0pphPhEQyuwsd7qsdlhjPBEQymxlg0tdUjNNJwJCuU13qctpx48NJwJCua3ynyDldIXZREAou/Pc6jIavN5oIiCU3buNrnUJXW8yERDKb4ZrXT7DWw0mAkL5rfafIOVzm7lEQIjBxe512ezRZiwREGLwwQ4udsncbSoREOJwiYtdLnu3G0oEhDh8NNDNLpUFZhIBIRaXudllMqbTSCIgxOLjQa52iSwykQgI8bjc1S6P8d0GEgEhHi1N7nZpLDGPCAgxucLdLosjTCMCQlTW7eRyl8RTphEBIS4/c7nL4RiziIAQmfVD3O5S+ItZRECIzbVudxmcZBIREKKzYWfXO7y6V00iAkJ8bnS+w5tmDhEQItQ6zP0OreFNc4iAEKM5Dnho55hCBIQofTbCBQ+r8V1TiIAQp1ud8LAuMoMICJHatIsbHtLAtWYQASFWcx3xkH5qAhEQotW2uyseTlOLCURAiNedzng4V5s/BISIbWl2x0MZusH8ISDE7G6HPJQ5pg8BIWrte7rkYezymelDQIjbPU55GPPMHgJC5Dr2dstDaN5i9hAQYnezY+43IAgIZOHX6EHcbfIQEASELO4weQgI0bvDMfcICwGBLHwVREAQEBAQAQEBoTg3OOYhzDJ5CAjRm+WYh/Bjk4eAEL2LHfMQzjN5CAjRO8cxD2GayUNAiN40xzyEKSYPASF6UxzzECaaPASE6E10zEPY3+QhIERvrGMewi4mDwEhers65iE0mDwEhOg1OuZBfGr0EBAit9EpD2Ol2UNAiNwqpzyMZWYPASFyy53yMBaaPQSEyP3RKQ/jFrOHgBC5u5zyMH5k9hAQIneFUx7Gd80eAkLkpjvlYRxi9hAQInesUx7GELOHgBC5UU55IC2GDwEhap3+ED2U500fAkLU3nfIQ7nf9CEgRO1phzyUq0wfAkLU5jvkoZxq+hAQojbLIQ9ltOlDQIjaVIc8lPo244eAEDP/ijecl40fAkLE2urd8WB+bf4QECL2N2c8nEvNHwJCxB50xsOZaP4QECLmH2EFNKjTACIgxGuyMx7QCgOIgBCv4a54QL80gAgI0VrtiIc0wwQiIETLB9GDGmsCERCi9TNHPKS6T40gAkKs/A49rMVGEAEhUp1NbnhQPzGDCAiR8nfogU0wgwgIkbrLCQ+rfp0hRECI05lOeGAPG0IEhDg1u+D+EgQBgQxWOuCh7W0KERCi5Hvo4b1tDBEQYjTN/Q7uDmOIgBChbm9SDO9oc4iAEKFXnO/wGjYYRASE+MxxvkvgQYOIgBCfia53CZxuEBEQorOu3vUugaY2o4iAEJsHHe9S+L1RRECIzffd7lL4vlFEQIjMlsFut2dYCAhk8JjTXRJeqIiAEJnzXO6SmGoYERCi0unP0MuiscU4IiDE5CmHuzTuNI4ICJ5gkcVhxhEBISJbhrjb5fGagURAiMcfXO0SmWkgERDicaqrXSLDNptIBIRYbNjB1S4Tr+RFQIjG3W52qUwykggIsTjUzS6X180kAkIcVrjYJXOhoURAiMNMF7tkmtabSgSEGLQNc7HL5lZjiYAQg/vd69IZ2WEuERAicLh7XT4LzSUCQvm94lqX0EEGEwGh/Ka71mX0J5OJgFB2HzY61mU02WgiIJTdz9zqcnrJbCIglFubTxGW1PcMJwJCud3jUpdU3QrTiYBQZl2jXOqyOsV4IiCU2cPudHn/E+RV84mAUF7dB7vT5XWiAUVAKK9HXekye8GEIiCU1hGOdJkdY0IREMrqcTfan6MjIJBB9wQnutzGd5lSBIRSWuxCl90CU4qAUMr/ABnvQJfdyM3mFAGhhBa5z+V3kzlFQCifjn2d5/Jr+tCkIiCUzjzXOQbTTSoCQtls9BreKNT9xawiIJTMVW5zHA71T3kREMrl3R2c5kjMN60ICKVyqsMci2EtxhUBoUSedpfjca55RUAoj05/QxjT79GXmVgEhNLwT3ijMtrfoyMglMWHOznKUbnSzCIglMT3neS4NPi6LQJCOTzpIkf3xyCdxhYBoQTa9nOQo+OliggIZTDLOY7PgBUGFwEhuFcanOMIHdxhdBEQAus42DGO0myzi4AQ2HVOcZzqnze8CAhBvdroFEdqr/XGFwEhoPZxDnG0Tje/CAgB+QpIzH5tgBEQgnmu3hWOWNNKI4yAEMjGvRzhqB3abogREMI42wmO3CxDjIAQxEIHOHZ1jxljBIQAVjY5wNHbeZVBRkAo3BZ/gp6CgzYZZQSEov3I8U3COUYZAaFgDzm9ifiFYUZAKNSbfgGSisYXjTMCQoFaxzq8yWhuMdAICIXpnubsJuRof0+IgFCYWxzdpFxgpBEQCvK4V2Al5i5DjYBQiLcHu7iJqX/KWCMgFGDDKAc3OYNXGGwEhNy1H+PcJmg//xQLASFv3Wc6tkk6zDtNEBByNsepTdTUTtONgJCn39S5tKm60HgjIOTo+UZ3Nl1zDDgCQm7eHu7KJqzuN0YcASEna/Z2ZJPWuNiQIyDkomWcE5u4QS8YcwSEHHx2mAObPH9QiICQA39AWAnDVxp1BIQa6zzZca2E5vcNOwJCTXX/0GmtiP0+MO4ICLXsxyUOa2WM+djAIyDoB1ns78WKCAg168dMR7VSxikIAoJ+kMkBnmIhINSEflTP2A/NPQJC3//741LntIJGrTH6CAh91HWBY1pJ+/h7EASEvunwAcKq2utd44+A0AebT3JIK6v5DQuAgJBZq/dfVdmw5VYAASGjdRMc0UpretISICBksuYAJ7TiBvzeGiAgZLCi2QGtvPp7LQICQq89Ndj5pF/dHKuAgNBLDzQ6nvzThZ22AQGhN26sczn5txM/sw8ICD3Wcb6zyX9N+MhKICD00KdHO5p8wd5vWgoEhB55fV8nky8Z+qy1QEDogcf88yu+qtE/50VA2L7b6p1Lvu5S/xgLAWHb2qY7lWzVCRusBwLCNrw73qHkG+z/jgVBQPhGj+7sTPKNhv3JiiAgbF3X7P6OJNtQf3O3NUFA2IpPp7iQbMe0VouCgPA1y0Y6j2zXuLetCgLCVx5f3djgONIDQx6xLQgIX/TRZJeRnqmb1WFhEBD+a+mu7iI9NukDK4OA8G+bZ3p3O70xfKmtQUD4pxX7u4j0Tv+rPMZCQPi869YB7iG9duR7dkdA/E9QdW8f5RaS6V9jLbI9AkKldd4+0CUko/M22SABobpeO8wVJLtRy+2QgFBRHdc1uoH0RcNsv0sXECrpr+McQPpqwkqbJCBUzuZZXl1CDQya12WbBIRqeXq000dtHLXKPgkIFfLhmc4eNbPjnb4SIiBURce8wY4etTTpLWslIFTCM355Tq3tcKN/jiUgpO+977l25OBAfxMiICRu/f/4y3Py0f/i9RZMQEhX+23D3Dlys9uDfpkuICSqe+Febhy5+vZr9kxASNHjB7lv5K3hIs+xBITkLD3CcaMIw3/u32MJCEk9vHpUPijM2CVWTkBIxiMeXlGobz1t6wSEJDx5pING0Y79s80TEKL3/LGOGSGc8JLtExCi9sqJDhmB1H13hQ0UEKL1xml1zhjh9J/2pi0UEKK06uz+Thhh1U9/xyYKCNFZM8MHBymBhvNX20YBISofz9zB6aIcGn+01kYKCNFYN6vJ2aI8Bl7WYisFhCi0XjvEyaJcmq5YZzMFhNJru2W4c0X5DJ690XYKCKXWfuduThXlNGxOqw0VEEqr45d7OlOU14i5bbZUQCilrgdHOVGU2x53tdtUAaF0uv84znmi/Pa8z+dCBISSeWKC00Qc9r2/y8IKCOWxbKKzRDzGPiQhAkJJLJ/sJBGXcYu7La6AEN6KqV65S3wmPG53BYTA3jrdK3eJk+/eCghBvXeuV+4SL9+9FRCCWXtxoxtE1Hz3VkAIouUnA90fYue7twJC8dZf7Y3tJMF3bwWEYrXeuLPDQyp891ZAKE7b3BGODinx3VsBoRjt85sdHFLTeLHv3goIeetcsLdjQ4p891ZAyFf3Q2McGlLVNMt3bwWE3Cw50JEhZb57KyDk5E+HOzCkbqjv3goItffnox0XqsB3bwWEGnt5isNCVezuu7cCQu28doo3tlMlvnsrINTIO2d5YztV47u3AkINrD7fG9upIt+9FRD66KMfD3BJqCjfvRUQ+uDTWTu6IlSY794KCBltnD3YBaHifPdWQMhg083DXA/od4zv3goIvdM+b1eXA/7Fd28FhF7ouGekqwH/UTfVd28FhJ7pemCUkwFf5Lu3AkJPdP/+AOcCvqr+bN+9FRC247FDnArYGt+9FRC26dlvORPwTXz3VkD4Ri8e50TAtvjurYCwVa+e5DzA9vjurYDwNW9O88pd6AnfvRUQvuTd6V65Cz3lu7cCwn99cEGjmwC9MOIO370VEP6h5dKB7gH0ku/eCgifr7+yyS2ADPa813dvBaTSWq8f4g5ARr57KyAV1nb7cDcA+mDsIgkRkEpqv3t3+w995Lu3AlJBHQv2svtQAxMec08EpFK6Fo2291AjvnsrIFWyeJydhxry3VsBqYqlE+w71Jjv3gpIFTx3lF2H2vPdWwFJ3kvHW3TIh+/eCkjS/v69OlsOufHdWwFJ1sozvLEd8tVw/vtOjYCkZ/V53tgO+fPdWwFJzoeXeGM7FGPgZZ84OQKSjpbLB9lqKIzv3gpIMjbM9sZ2KNbg2RucHgGJ32dzhtpmKJzv3gpI9LbM3cUmQxC+eysgUeuY32yLIRjfvRWQaHXdv68NhqB891ZAotT98FjbC8H57q2AxOeRg20ulILv3gpIXJ4+0tZCafjurYDE44VjbSyUyqG+eysgUfjbibYVSsd3bwWk/N44zRvboZSO9t1bASm1VdO9sR1Ky3dvBaS81szwxnYoM9+9FZCS+mTmDvYTSs53bwWkhNbP8spdiCIhvnsrIOXSeu0QewmR8N1bASmRtluH20mIiO/eCkhJtN+1m32EyPjurYCUQMev9rSLECHfvRWQwLoWjrKHECnfvRWQgLoXj7ODEDHfvRWQUJ6YYP8gciNu991bASneskl2DxLgu7cCUrTlk+0dJGLPe3z3VkCKs2KqV+5CQnz3VkCKsvJ0r9yFxIzx3VsBKcD753rlLiTId28FJG9rL260aJAm370VkDy1/HSgJYN0+e6tgORl/TXe2A6J891bAclD6007Wy5In+/eCkitbZ47wmJBJfjurYDUVPv8ZlsFldF/2hvOnoDURueCfWwUVCshvnsrILXQ/dBY2wSV0/BD370VkL5aMt4mQSX57q2A9M2Th9siqKyBl/rurYBk9fzRNggqzXdvBSSbl6fYHqg8370VkN577VRvbAf6+e6tgPTWO2d7Yzvwf3z3VkB6bs353tgOfIHv3gpIz3w8c4B1Ab7Md28FZPvWzfLKXWArfPdWQLat9doh1gTYurEP+WihgHyTttuGWxHgm/nurYBsXfvPd7cewLZNeMKxFJCv6liwl9UAtm/iMw6mgHxR16Ix1gLomWOfdzQF5L+WHGglgJ6b8rK7KSD/4pW7QC/Vney7twLilbtAJv1Pf0tAKv7//ytT7AGQScP0dwWkwt44zSt3gcwaZ6wRkIpaNd0rd4E+GfDjjwSkgj6Y0Wj4gb4adHmLgFRMy8yBBh+ohaafrReQCll/lVfuAjUz5PpWAamI1hu9cheoqeG3tglIBbTN9cpdoOZ2nbdFQBLXMb/ZoAN5aJ7fISAJ61qwjyEH8rLPgg4BSVT3w2MNOJCn0b/tEpAUPXKw4QbyNu6P3QKSmqePNNhAEQ59VECS8uJxhhooypFPCkgyXj3JQANFOvrPApKEt6Z5ZyJQtOOXC0j03junwSQDxaubukJAorb2Iq/cBQLpP+1NAYlWy0+9chcIqP7sdwQkSuuv8cpdILCG81cLSHQ+mzPU6ALhNV7yoYBEZcu8XYwtUA4DL2sRkGh03OOVu0CJNF25XkCi0PXAfsYVKJch124UkNLr/v0BRhUon2FzPhOQcnv8UGMKlNMuczcLSHktm2hEgfJq/kW7gJTT8snGEyi3vX7VISDls2JqndkESm/Ug10CUi4rz/DKXSAOBzzcLSDlsfo8r9wF4nHQEgEpiY8u8cpdIC6H/0lASuDTyweZRSA6k5YJSGAbZ+9kDoEoTV4uIAFtunm4GQSiddLfBCSQ9nm7mT8gZnWnviYgAXT8ck/DB8Su/1krBaRgXQtHGTwgBQ3nvScgBepePM7QAalovPADASnK0gkGDkjJwJmfCEgRnptk2IDUNM36VEDy9vIJBg1I0U6zNwhInl472St3gVQNndMqIHl55yyv3AVSNuL2NgHJw5oZXrkLpG73u9oFpNY+njnAZAEVsOd9HQJSS+tnNZkqoCL2u79LQGql9dohJgqokLEPdQtILbTd5pW7QNUcuKRbQPqq/e7dTRJQQROeEJA+6ViwlykCKmriswKSWfeiMSYIqLDjXhCQbJYcaHqAipvysoD03pOHmxyAupP/LiC98/wxxgbgn/p//y0B6blXphgZgP9omP6ugPTMG6d55S7AFzXOWCMg27dqer1ZAfiKHX78kYBs2wczGs0JwFYMurxFQL5Zy6UDzQjAN2i6Zr2AbN36q7xyF2Bbdr6+VUC+rvVGr9wF2J7ht7YJyJe1zR1hLgB6YLd5WwTkC+9MnN9sJgB6qHl+h4D8W9f9+5gHgF7YZ0GngHz+effDY80CQC+NWdRV+YA8fog5AMhg3OLuSgdk2UQzAJBR4I8WBg3IS8f7+QP0waRl1QzI66d4ZyJAH01eXr2ArDq7vx88QN9NXVGtgHxwgXcmAtRG/2lvVicgLT/xzkSA2qkP8sWpAAHZcI13JgLUVogvThUekE03D/OTBqi5HWZ+knZA2u/czU8ZIBdNs9alG5COX+/lJwyQm8GzW9MMSPdDY/x0AXI1/OZNCQbkkfF+sgC527Wwz4UUFZBnvuWnClCIkfd0JBSQ5ZP9RAEKs+/9XYkE5O/f9dIrgEKNfbg7gYC8c5aXXgEU7qAlsQdkzYwGP0aAEI54MuaAtMz00iuAYI7+c6wBWX+1l14BBHXCyzEG5LM5O/vRAQRWd/LfYwtI+7xd/dwASqD/GStjCkjHL0f6mQGURMN578cSkK5Fo/y8AEqk8eK1UQRkyYF+VgAlM/CnLaUPyHMT/ZwASqjpZ+tLHZAVJ/oZAZTUkBtbSxuQVWd6awlAiQ2/va2UAfno4kY/HIBy2/3u9tIFZP1V/uwcIAJ7/aqjVAFpu3WYHwpAHEYt7CpNQDrubfYDAYjHuD92lyIg3Q+N9sMAiMuhj5cgIEsP9YMAiM/EZ0MH5Fg/BIA4HRc4IH4CALESEAAEBAABAUBABARAQAQEAAEBQEAAEBAABERAAAREQAAQEAAEBAABAUBABARAQAQEAAEBQEAAEBAABERAAAREQAAQEAAEBAABERAAAREQAAEREAAEBAABAUBABARAQAQEQEAEBAABAUBAABAQAQEQEAEBEBABAUBAABAQAAREQAAEREAAEBAABAQAAQFAQAQEQEAEBAABAUBAABAQAAREQAAEREAAEBAABAQAAQFAQAQEQEAEBAABAUBAABAQAAREQAAEREAAEBAABAQAAREQAAEREAABERAABAQAAQFAQAQEQEAEBEBABAQAAQFAQAAQEAEBEBABARAQAQFAQAAQEAAEREAABERAABAQAAQEAAEBQEAEBEBABAQAAQFAQAAQEAAEREAABERAABAQAAQEAAEBQEAEBEBABAQAAQFAQAAQEAEBEBABARAQAQFAQAAQEAAEREAABERAAAREQAAQEAAEBAABERAAAREQAAEREAAEBAABAUBABARAQAQEAAEBQEAAEBAABERAAAREQAAQEABCBuR/AXZ8so6QOO3WAAAAAElFTkSuQmCC", "838566e2-6552-4b02-a7cf-21ead74c6b2a", false, "customer1" },
                    { new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"), 0, "018991f4-38b1-4ad8-9667-66ad68263788", null, "admin1@example.com", true, "Main", "Admin", false, null, "ADMIN1@EXAMPLE.COM", "ADMIN1", "AQAAAAIAAYagAAAAEJPJK+vAWnoLTbmieCsiCHWHvOWNUt/ccBB5Coq8YuYu4d3kldpSYzD5BeBX2mLdww==", null, false, "data:image/png;base64,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", "49d48c8f-d22a-4642-97de-5350e4b54fae", false, "admin1" },
                    { new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"), 0, "678c0851-2649-4369-bf55-f7b7fc5b8caa", null, "admin2@example.com", true, "Second", "Admin", false, null, "ADMIN2@EXAMPLE.COM", "ADMIN2", "AQAAAAIAAYagAAAAEGRoQTV2pHK/HgID46gd+EsckPmldCNKAYIFlccqHx+/oRNeY4jqaIpQdcOkY6AKHQ==", null, false, "data:image/png;base64,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", "e3b48e5e-fa64-437f-8f0f-9e6208845fe3", false, "admin2" }
                });

            migrationBuilder.InsertData(
                table: "addresses",
                columns: new[] { "id", "city", "customer_id", "postal_code", "street_and_number" },
                values: new object[] { new Guid("53cab8a2-000d-48c3-a6dd-602168b851a2"), "Beograd", null, "11000", "Knez Mihailova 12" });

            migrationBuilder.InsertData(
                table: "allergens",
                columns: new[] { "id", "name", "type" },
                values: new object[,]
                {
                    { new Guid("08d0fb3a-739b-4e00-a356-b3ae2ef6ba5d"), "Jagode", "Voće" },
                    { new Guid("1b312674-61f7-4530-9480-3cc4536590dc"), "Banane", "Voće" },
                    { new Guid("25db7332-9e79-41b2-b1b9-d51cf88b4959"), "Breskve", "Voće" },
                    { new Guid("276bf8d4-ef5e-4e3b-9f59-a0d1cbecc3ba"), "Kikiriki", "Orašasti plodovi" },
                    { new Guid("5334ebc0-d7e2-449e-91fa-9f1886c62f2d"), "Pšenica", "Žitarice" },
                    { new Guid("5d078b13-cb34-498c-8d69-9b2f1dd40320"), "Paradajz", "Povrće" },
                    { new Guid("850359e5-3311-4cfb-963d-72c754b99797"), "Školjke", "Morski plodovi" },
                    { new Guid("a20b23d6-92d3-4cde-8786-ffffb56ee4fe"), "Kivi", "Voće" },
                    { new Guid("af3a4d60-bcd8-49dc-a478-e7f45881da82"), "Celer", "Povrće" },
                    { new Guid("c8217a2e-9256-426f-97c6-a02b3124c558"), "Gluten", "Žitarice" }
                });

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
                columns: new[] { "id", "address_id", "description", "image", "name", "owner_id", "phone_number" },
                values: new object[] { new Guid("a671a0ce-3d0c-446d-b2f7-a1388e15136d"), new Guid("53cab8a2-000d-48c3-a6dd-602168b851a2"), "Autentična italijanska kuhinja.", "", "Pizzeria Roma", new Guid("33333333-3333-3333-3333-333333333333"), "222" });

            migrationBuilder.InsertData(
                table: "vouchers",
                columns: new[] { "id", "active", "code", "customer_id", "date_issued", "discount_amount", "expiration_date", "name" },
                values: new object[] { new Guid("e4cdb246-e85b-4260-8aa4-d2c922e59c59"), true, "AN0JB", new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 10, 19, 20, 38, 8, 321, DateTimeKind.Utc).AddTicks(2162), 1200.0, new DateTime(2025, 10, 20, 20, 38, 8, 321, DateTimeKind.Utc).AddTicks(2154), "TestVaučer" });

            migrationBuilder.InsertData(
                table: "base_work_scheds",
                columns: new[] { "id", "restaurant_id", "saturday", "sunday", "weekend_end", "weekend_start", "work_day_end", "work_day_start" },
                values: new object[] { new Guid("9fd31f78-e0be-4bad-9941-7adaba0eeaf4"), new Guid("a671a0ce-3d0c-446d-b2f7-a1388e15136d"), true, true, new TimeSpan(0, 21, 30, 0, 0), new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "id", "name", "restaurant_id" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), "Pizza Menu", new Guid("a671a0ce-3d0c-446d-b2f7-a1388e15136d") });

            migrationBuilder.InsertData(
                table: "dishes",
                columns: new[] { "id", "description", "menu_id", "name", "picture_url", "price", "type" },
                values: new object[] { new Guid("c0fb25cb-9d29-4272-a293-823f6ba1b602"), "Pica sa šunkom i sirom.", new Guid("55555555-5555-5555-5555-555555555555"), "Capricciosa", null, 750.0, "Pizza" });

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
                name: "ix_vouchers_code",
                table: "vouchers",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_vouchers_customer_id",
                table: "vouchers",
                column: "customer_id");

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
                name: "vouchers");

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
