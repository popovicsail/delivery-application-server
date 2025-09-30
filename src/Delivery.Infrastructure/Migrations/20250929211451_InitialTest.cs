using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialTest : Migration
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
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                    { new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"), 0, "b77bb66c-c715-4af0-af55-617bbaef75d0", "admin@example3.com", true, "Glavni", "Admin", false, null, "ADMIN@EXAMPLE3.COM", "ADMIN3", "AQAAAAIAAYagAAAAEFPsx9VlgObfO0eae2BFEzrwZnPtvengNYP+bY4kPonDZnr9fTinBqeEaXUihyEN3Q==", null, true, null, null, false, "admin3" },
                    { new Guid("42307445-f995-4e66-8805-d21ea5bf91ea"), 0, "12b58997-9433-4901-abbb-ce7e4dbcde1a", "customer1@example.com", true, "Peter", "Peterson", false, null, "CUSTOMER1@EXAMPLE.COM", "CUSTOMER1", "AQAAAAIAAYagAAAAEHNGnr6DV6k8GKLPEnDy9FBKqPlTDFz+nryMr+UlmtLBHDSnkP/uve0WU8hFkYBieA==", null, true, null, null, false, "customer1" },
                    { new Guid("a37d42af-4299-4fe1-8981-476033da04b1"), 0, "ac4752f5-1aa9-462c-9e74-60854a6fbdba", "owner1@example.com", true, "Mark", "Markov", false, null, "OWNER1@EXAMPLE.COM", "OWNER1", "AQAAAAIAAYagAAAAEOp8F/t4DIn1h5FQydzSEqqwDtwR3/+LQZMTLkjEqFFvGucorXgS/wI/hmKwR6pUiQ==", null, true, null, null, false, "owner1" },
                    { new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"), 0, "cc84c1bd-ab46-46e5-862d-8afe995bbcff", "admin@example1.com", true, "Glavni", "Admin", false, null, "ADMIN@EXAMPLE1.COM", "ADMIN1", "AQAAAAIAAYagAAAAECV2XzNSSI08yAD90K4G+OAOMwNwZ1RvtMz9Mz6MNetdN1lJ/Vl25/SDCdecoEf/iw==", null, true, null, null, false, "admin1" },
                    { new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"), 0, "00599ffd-b785-4028-a847-297e78f8119c", "admin@example2.com", true, "Glavni", "Admin", false, null, "ADMIN@EXAMPLE2.COM", "ADMIN2", "AQAAAAIAAYagAAAAEGwPqkQw9rmIyFLwQmEAZeRQNtvRrBQmlL36pWgFEucWBedZ3WQb/+GQ5lBlyVr4bg==", null, true, null, null, false, "admin2" }
                });

            migrationBuilder.InsertData(
                table: "addresses",
                columns: new[] { "id", "city", "customer_id", "postal_code", "street_and_number" },
                values: new object[,]
                {
                    { new Guid("8e563b1b-6465-4ff4-8b1b-0d9734ed7328"), "London", null, "NW1 6XE", "221B Baker Street" },
                    { new Guid("9354b0e3-81a2-4604-9876-c756639e0d03"), "New York", null, "10001", "123 Main St" }
                });

            migrationBuilder.InsertData(
                table: "allergens",
                columns: new[] { "id", "name", "type" },
                values: new object[,]
                {
                    { new Guid("a97b8181-b6be-4634-b5b6-1a3f27435257"), "Gluten", "Cereals" },
                    { new Guid("bc0f5ab6-7c91-4e8f-82e9-e3f3460e8288"), "Lactose", "Dairy" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("1ddc68db-bb87-4cef-bdf8-d369bc1d5334") },
                    { new Guid("5b00155d-77a2-438c-b18f-dc1cc8af5a43"), new Guid("42307445-f995-4e66-8805-d21ea5bf91ea") },
                    { new Guid("fc7e84f2-e37e-46e2-a222-a839d3e1a3bb"), new Guid("a37d42af-4299-4fe1-8981-476033da04b1") },
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7") },
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), new Guid("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f") }
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "id", "user_id" },
                values: new object[] { new Guid("f75fd0bb-f604-4864-80b1-d8b9597c5dde"), new Guid("42307445-f995-4e66-8805-d21ea5bf91ea") });

            migrationBuilder.InsertData(
                table: "owners",
                columns: new[] { "id", "user_id" },
                values: new object[] { new Guid("aaf6932f-2729-49fe-b1c9-0c621744a2d5"), new Guid("a37d42af-4299-4fe1-8981-476033da04b1") });

            migrationBuilder.InsertData(
                table: "allergen_customer",
                columns: new[] { "allergens_id", "customers_id" },
                values: new object[] { new Guid("bc0f5ab6-7c91-4e8f-82e9-e3f3460e8288"), new Guid("f75fd0bb-f604-4864-80b1-d8b9597c5dde") });

            migrationBuilder.InsertData(
                table: "restaurants",
                columns: new[] { "id", "address_id", "description", "name", "owner_id" },
                values: new object[] { new Guid("3c3aa82c-72ab-494c-8c78-35042bc412ea"), new Guid("9354b0e3-81a2-4604-9876-c756639e0d03"), "The best grill in town.", "The Gilded Spoon", new Guid("aaf6932f-2729-49fe-b1c9-0c621744a2d5") });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "id", "name", "restaurant_id" },
                values: new object[] { new Guid("f50070bd-fc42-4bee-ad85-b4c63544001b"), "Main Menu", new Guid("3c3aa82c-72ab-494c-8c78-35042bc412ea") });

            migrationBuilder.InsertData(
                table: "dishes",
                columns: new[] { "id", "description", "menu_id", "name", "picture_url", "price", "type" },
                values: new object[,]
                {
                    { new Guid("25ba4ed1-e478-4b38-bde6-f6249d6c581b"), "A timeless classic", new Guid("f50070bd-fc42-4bee-ad85-b4c63544001b"), "Capricciosa", null, 12.5, "Pizza" },
                    { new Guid("3d07ff67-852c-4b43-813e-7b727d0cc10f"), "Cream and bacon", new Guid("f50070bd-fc42-4bee-ad85-b4c63544001b"), "Carbonara", null, 10.5, "Pasta" }
                });

            migrationBuilder.InsertData(
                table: "allergen_dish",
                columns: new[] { "allergens_id", "dishes_id" },
                values: new object[,]
                {
                    { new Guid("a97b8181-b6be-4634-b5b6-1a3f27435257"), new Guid("25ba4ed1-e478-4b38-bde6-f6249d6c581b") },
                    { new Guid("bc0f5ab6-7c91-4e8f-82e9-e3f3460e8288"), new Guid("25ba4ed1-e478-4b38-bde6-f6249d6c581b") }
                });

            migrationBuilder.InsertData(
                table: "dish_option_groups",
                columns: new[] { "id", "dish_id", "name" },
                values: new object[] { new Guid("d1834f99-1605-4ba9-ba11-b69e2ee9c801"), new Guid("25ba4ed1-e478-4b38-bde6-f6249d6c581b"), "Pizza Toppings" });

            migrationBuilder.InsertData(
                table: "dish_options",
                columns: new[] { "id", "dish_option_group_id", "name", "price" },
                values: new object[,]
                {
                    { new Guid("236c1acc-fff0-4cc1-bb2e-0905c812f60c"), new Guid("d1834f99-1605-4ba9-ba11-b69e2ee9c801"), "Oregano", 0.29999999999999999 },
                    { new Guid("d942d5fb-9161-438f-b951-6c8c2a519674"), new Guid("d1834f99-1605-4ba9-ba11-b69e2ee9c801"), "Ketchup", 0.5 }
                });

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
