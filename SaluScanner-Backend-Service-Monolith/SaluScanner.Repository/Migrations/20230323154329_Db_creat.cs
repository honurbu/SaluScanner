using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaluScanner.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Dbcreat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponentDescription",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "AllergenSymptoms",
                table: "Allergens",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllergenSymptoms",
                table: "Allergens");

            migrationBuilder.AddColumn<string>(
                name: "ComponentDescription",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
