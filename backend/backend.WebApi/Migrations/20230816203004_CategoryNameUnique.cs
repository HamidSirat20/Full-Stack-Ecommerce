using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class CategoryNameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_categories_category_name",
                table: "categories",
                column: "category_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_categories_category_name",
                table: "categories");
        }
    }
}
