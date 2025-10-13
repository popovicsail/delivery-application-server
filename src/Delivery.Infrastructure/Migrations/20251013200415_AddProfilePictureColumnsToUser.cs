using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePictureColumnsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profile_picture_url",
                table: "AspNetUsers",
                newName: "profile_picture_mime_type");

            migrationBuilder.AddColumn<string>(
                name: "profile_picture_base64",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_picture_base64",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "profile_picture_mime_type",
                table: "AspNetUsers",
                newName: "profile_picture_url");
        }

    }
}
