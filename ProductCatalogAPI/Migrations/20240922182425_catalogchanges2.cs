using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class catalogchanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Catalog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Amazon",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Catalog",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Amazon");
        }
    }
}
