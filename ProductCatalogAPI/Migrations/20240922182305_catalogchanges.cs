using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class catalogchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Catalog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Catalog");
        }
    }
}
