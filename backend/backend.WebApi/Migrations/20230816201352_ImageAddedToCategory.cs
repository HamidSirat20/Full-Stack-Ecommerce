using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ImageAddedToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "categories");
        }
    }
}
