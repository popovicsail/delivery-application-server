using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "order_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "order_items");

        }
    }
}
